
using Dapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Base.TimePeriod;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using System.Data;

namespace OrderAutomationsProject.Operation.Operations.ReportOperations.Commands;

public class CreateDealerReportCommandHandler : IRequestHandler<CreateDealerReportCommand, ApiResponse<List<int>>>
{
    private readonly IDbConnection dbConnection;
    private readonly ISessionService sessionService;

    public CreateDealerReportCommandHandler(IDbConnection dbConnection, ISessionService sessionService)
    {
        this.dbConnection = dbConnection;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<int>>> Handle(CreateDealerReportCommand request, CancellationToken cancellationToken)
    {
        TimePeriodType timePeriod = FromString(request.Model.TimePeriod);

        if(sessionService.CheckSession().sessionRole == "admin")
        {
            if (timePeriod == TimePeriodType.Daily)
            {
                List<int> rep = GetLast7DaysOrderCounts(request.Model.DealerId);
                return new ApiResponse<List<int>>(rep);
            }
            else if (timePeriod == TimePeriodType.Monthly)
            {
                List<int> rep = GetLast4WeeksOrderCounts(request.Model.DealerId);
                return new ApiResponse<List<int>>(rep);
            }
            else if (timePeriod == TimePeriodType.Weekly)
            {
                List<int> rep = GetLast12MonthsOrderCounts(request.Model.DealerId);
                return new ApiResponse<List<int>>(rep);
            }
        }

        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            var sessionId = sessionService.CheckSession().sessionId;
            if (timePeriod == TimePeriodType.Daily)
            {
                List<int> rep = GetLast7DaysOrderCounts(sessionId);
                return new ApiResponse<List<int>>(rep);
            }
            else if (timePeriod == TimePeriodType.Monthly)
            {
                List<int> rep = GetLast4WeeksOrderCounts(sessionId);
                return new ApiResponse<List<int>>(rep);
            }
            else if (timePeriod == TimePeriodType.Weekly)
            {
                List<int> rep = GetLast12MonthsOrderCounts(sessionId);
                return new ApiResponse<List<int>>(rep);
            }
        }

        throw new InvalidOperationException("Invalid time period: " + request.Model.TimePeriod);
    }

    private List<int> GetLast7DaysOrderCounts(int id)
    {
        List<int> orderCounts = new List<int>();
        DateTime today = DateTime.Today;

        for (int i = 0; i < 7; i++)
        {
            DateTime startDate = today.AddDays(-i);
            DateTime endDate = startDate.AddDays(1);

            string sql = "SELECT COUNT(*) FROM \"Order\" WHERE IsActive = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
            int orderCount = dbConnection.QuerySingle<int>(sql, new { StartDate = startDate, EndDate = endDate, DealerId = id });
            orderCounts.Add(orderCount);
        }

        return orderCounts;
    }

    private List<int> GetLast4WeeksOrderCounts(int id)
    {
        List<int> orderCounts = new List<int>();
        DateTime today = DateTime.Today;

        for (int i = 0; i < 4; i++)
        {
            DateTime startOfWeek = today.AddDays(-i * 7);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            string sql = "SELECT COUNT(*) FROM \"Order\" WHERE IsActive = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
            int orderCount = dbConnection.QuerySingle<int>(sql, new { StartDate = startOfWeek, EndDate = endOfWeek, DealerId = id });
            orderCounts.Add(orderCount);
        }

        return orderCounts;
    }

    private List<int> GetLast12MonthsOrderCounts(int id)
    {
        List<int> orderCounts = new List<int>();
        DateTime today = DateTime.Today;

        for (int i = 0; i < 12; i++)
        {
            DateTime startOfMonth = today.AddMonths(-i).Date.AddDays(1 - today.Day);
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            string sql = "SELECT COUNT(*) FROM \"Order\" WHERE IsActive = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
            int orderCount = dbConnection.QuerySingle<int>(sql, new { StartDate = startOfMonth, EndDate = endOfMonth, DealerId = id });
            orderCounts.Add(orderCount);
        }

        return orderCounts;
    }

    private ApiResponse<ReportResponse> CreateDailyReport()
    {
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);

        // Dapper kullanarak sorguyu çalıştırma
        string sql = "SELECT * FROM \"Order\" WHERE IsActive = 1 AND Confirmation = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
        List<Order> ordersList = dbConnection.Query<Order>(sql, new { StartDate = today, EndDate = tomorrow }).ToList();

        decimal orderIntensity = CalculateOrderIntensity(ordersList, 50);

        return CreateReportResponse(ordersList, orderIntensity);
    }

    private ApiResponse<ReportResponse> CreateMonthlyReport()
    {
        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);

        // Dapper kullanarak sorguyu çalıştırma
        string sql = "SELECT * FROM \"Order\" WHERE IsActive = 1 AND Confirmation = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
        List<Order> ordersList = dbConnection.Query<Order>(sql, new { StartDate = firstDayOfMonth, EndDate = firstDayOfNextMonth }).ToList();

        decimal orderIntensity = CalculateOrderIntensity(ordersList, 1000);

        return CreateReportResponse(ordersList, orderIntensity);
    }

    private ApiResponse<ReportResponse> CreateWeeklyReport()
    {
        DateTime today = DateTime.Today;
        DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        DateTime endOfWeek = startOfWeek.AddDays(7);

        // Dapper kullanarak sorguyu çalıştırma
        string sql = "SELECT * FROM \"Order\" WHERE IsActive = 1 AND Confirmation = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
        List<Order> ordersList = dbConnection.Query<Order>(sql, new { StartDate = startOfWeek, EndDate = endOfWeek }).ToList();

        decimal orderIntensity = CalculateOrderIntensity(ordersList, 250);

        return CreateReportResponse(ordersList, orderIntensity);
    }

    private decimal CalculateOrderIntensity(List<Order> ordersList, int capacity)
    {
        int ordersTotalQuantity = ordersList.Sum(order => order.AllQuantity);
        return ordersTotalQuantity / capacity * 100;
    }

    private ApiResponse<ReportResponse> CreateReportResponse(List<Order> ordersList, decimal orderIntensity)
    {
        ReportResponse response = new ReportResponse
        {
            Orders = ordersList,
            OrderIntensity = orderIntensity
        };
        return new ApiResponse<ReportResponse>(response);
    }

    public static TimePeriodType FromString(string timePeriodTypeString)
    {
        if (Enum.TryParse(timePeriodTypeString, out TimePeriodType timePeriodTyp))
        {
            return timePeriodTyp;
        }

        throw new InvalidOperationException("Invalid time period: " + timePeriodTypeString);
    }
}