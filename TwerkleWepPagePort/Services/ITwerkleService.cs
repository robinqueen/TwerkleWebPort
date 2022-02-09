using TwerkleWepPagePort.Models;

namespace TwerkleWepPagePort.Services
{
    public interface ITwerkleService
    {
        Task<string> GetWordOfDay();
    }
}
