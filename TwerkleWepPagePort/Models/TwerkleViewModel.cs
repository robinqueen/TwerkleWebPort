using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwerkleWepPagePort.Services;

namespace TwerkleWepPagePort.Models
{
    public class TwerkleViewModel : PageModel
    {
        [BindProperty]
        public WordAttempts wordAttempts { get; set; }

    }
}
