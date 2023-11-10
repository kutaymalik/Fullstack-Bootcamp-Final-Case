using Azure.Core;
using Dapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Base.TimePeriod;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using System.Data;
using static Dapper.SqlMapper;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, ApiResponse<List<int>>>
{
    private readonly IDbConnection dbConnection;
    private readonly ISessionService sessionService;

    public CreateReportCommandHandler(IDbConnection dbConnection, ISessionService sessionService)
    {
        this.dbConnection = dbConnection;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<int>>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        TimePeriodType timePeriod = FromString(request.Model.TimePeriod);


        if (timePeriod == TimePeriodType.Daily)
        {
            List<int> rep = GetLast7DaysOrderCounts();
            return new ApiResponse<List<int>>(rep);
        }
        else if (timePeriod == TimePeriodType.Monthly)
        {
            List<int> rep = GetLast4WeeksOrderCounts();
            return new ApiResponse<List<int>>(rep);
        }
        else if (timePeriod == TimePeriodType.Weekly)
        {
            List<int> rep = GetLast12MonthsOrderCounts();
            return new ApiResponse<List<int>>(rep);
        }

        throw new InvalidOperationException("Invalid time period: " + request.Model.TimePeriod);
    }

    public static TimePeriodType FromString(string timePeriodTypeString)
    {
        if (Enum.TryParse(timePeriodTypeString, out TimePeriodType timePeriodTyp))
        {
            return timePeriodTyp;
        }

        throw new InvalidOperationException("Invalid time period: " + timePeriodTypeString);
    }

    private List<int> GetLast7DaysOrderCounts()
    {
        List<int> orderCounts = new List<int>();
        DateTime today = DateTime.Today;

        for (int i = 0; i < 7; i++)
        {
            DateTime startDate = today.AddDays(-i);
            DateTime endDate = startDate.AddDays(1);

            string sql = "SELECT COUNT(*) FROM \"Order\" WHERE IsActive = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate";
            int orderCount = dbConnection.QuerySingle<int>(sql, new { StartDate = startDate, EndDate = endDate });
            orderCounts.Add(orderCount);
        }

        return orderCounts;
    }

    private List<int> GetLast4WeeksOrderCounts()
    {
        List<int> orderCounts = new List<int>();
        DateTime today = DateTime.Today;

        for (int i = 0; i < 4; i++)
        {
            DateTime startOfWeek = today.AddDays(-i * 7);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            string sql = "SELECT COUNT(*) FROM \"Order\" WHERE IsActive = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate";
            int orderCount = dbConnection.QuerySingle<int>(sql, new { StartDate = startOfWeek, EndDate = endOfWeek });
            orderCounts.Add(orderCount);
        }

        return orderCounts;
    }

    private List<int> GetLast12MonthsOrderCounts()
    {
        List<int> orderCounts = new List<int>();
        DateTime today = DateTime.Today;

        for (int i = 0; i < 12; i++)
        {
            DateTime startOfMonth = today.AddMonths(-i).Date.AddDays(1 - today.Day);
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            string sql = "SELECT COUNT(*) FROM \"Order\" WHERE IsActive = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate";
            int orderCount = dbConnection.QuerySingle<int>(sql, new { StartDate = startOfMonth, EndDate = endOfMonth });
            orderCounts.Add(orderCount);
        }

        return orderCounts;
    }

    //private ApiResponse<ReportResponse> CreateDailyReport(int dealerId)
    //{
    //    DateTime today = DateTime.Today;
    //    DateTime tomorrow = today.AddDays(1);

    //    // Run the query using Dapper
    //    string sql = "SELECT * FROM Order WHERE IsActive = 1 AND Confirmation = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
    //    List<Order> ordersList = dbConnection.Query<Order>(sql, new { StartDate = today, EndDate = tomorrow, DealerId = dealerId}).ToList();

    //    decimal orderIntensity = CalculateOrderIntensity(ordersList, 50);

    //    return CreateReportResponse(ordersList, orderIntensity);
    //}

    //private ApiResponse<ReportResponse> CreateMonthlyReport(int dealerId)
    //{
    //    DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    //    DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);

    //    // Run the query using Dapper
    //    string sql = "SELECT * FROM Order WHERE IsActive = 1 AND Confirmation = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
    //    List<Order> ordersList = dbConnection.Query<Order>(sql, new { StartDate = firstDayOfMonth, EndDate = firstDayOfNextMonth, DealerId = dealerId }).ToList();

    //    decimal orderIntensity = CalculateOrderIntensity(ordersList, 1000);

    //    return CreateReportResponse(ordersList, orderIntensity);
    //}

    //private ApiResponse<ReportResponse> CreateWeeklyReport(int dealerId)
    //{
    //    DateTime today = DateTime.Today;
    //    DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
    //    DateTime endOfWeek = startOfWeek.AddDays(7);

    //    // Run the query using Dapper
    //    string sql = "SELECT * FROM Order WHERE IsActive = 1 AND Confirmation = 1 AND InsertDate >= @StartDate AND InsertDate < @EndDate AND DealerId = @DealerId";
    //    List<Order> ordersList = dbConnection.Query<Order>(sql, new { StartDate = startOfWeek, EndDate = endOfWeek, DealerId = dealerId }).ToList();

    //    decimal orderIntensity = CalculateOrderIntensity(ordersList, 250);

    //    return CreateReportResponse(ordersList, orderIntensity);
    //}

    //private decimal CalculateOrderIntensity(List<Order> ordersList, int capacity)
    //{
    //    int ordersTotalQuantity = ordersList.Sum(order => order.AllQuantity);
    //    return ordersTotalQuantity / capacity * 100;
    //}

    //private ApiResponse<ReportResponse> CreateReportResponse(List<Order> ordersList, decimal orderIntensity)
    //{
    //    ReportResponse response = new ReportResponse
    //    {
    //        Orders = ordersList,
    //        OrderIntensity = orderIntensity
    //    };
    //    return new ApiResponse<ReportResponse>(response);
    //}

}