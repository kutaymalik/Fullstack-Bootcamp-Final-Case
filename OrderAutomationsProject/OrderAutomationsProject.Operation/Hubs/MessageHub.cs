using Microsoft.AspNetCore.SignalR;
using OrderAutomationsProject.Data.Context;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using System.Security.Claims;

namespace OrderAutomationsProject.Operation.Hubs;

public class MessageHub : Hub
{
    private readonly IUnitOfWork unitOfWork;
    private readonly OaDbContext oaDbContext;
    private readonly ISessionService sessionService;


    public MessageHub(IUnitOfWork unitOfWork, ISessionService sessionService, OaDbContext oaDbContext)
    {
        this.unitOfWork = unitOfWork;
        this.sessionService = sessionService;
        this.oaDbContext = oaDbContext;
    }
    public async Task SendMessage(string senderId ,string receiverId, string content, string role)
    {

        try
        {
            var sender = unitOfWork.DealerRepository.FirstOrDefault(x => x.Id == int.Parse(senderId));

            if(sender.Role == "dealer")
            {
                var admin = unitOfWork.DealerRepository.FirstOrDefault(x => x.Role == "admin");

                var message = new Message
                {
                    SenderId = int.Parse(senderId),
                    ReceiverId = int.Parse(receiverId),
                    Content = content,
                    SentAt = DateTime.Now,
                    Role = role
                };

                oaDbContext.Set<Message>().Add(message);
                oaDbContext.SaveChanges();

                await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content);

            } else
            {
                var receiver = unitOfWork.DealerRepository.FirstOrDefault(x => x.Id == int.Parse(receiverId));

                var message = new Message
                {
                    SenderId = int.Parse(senderId),
                    ReceiverId = int.Parse(receiverId),
                    Content = content,
                    SentAt = DateTime.Now,
                    Role = role
                };

                oaDbContext.Set<Message>().Add(message);
                oaDbContext.SaveChanges();

                await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"SendMessage metodu içinde bir hata oluştu: {ex.InnerException}");
        }
        
    }
}
