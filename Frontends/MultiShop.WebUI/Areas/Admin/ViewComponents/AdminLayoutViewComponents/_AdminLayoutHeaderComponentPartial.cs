using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentStatisticService _commentStatisticService;
        private readonly ICommentService _commentService;

        public _AdminLayoutHeaderComponentPartial(IMessageService messageService, IUserService userService, ICommentStatisticService commentStatisticService, ICommentService commentService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentStatisticService = commentStatisticService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();
            int messageCount = await _messageService.GetTotalMessageCountByReceiverId(user.Id);
            ViewBag.messageCount = messageCount;

            int totalCommentCount = await _commentStatisticService.GetTotalCommentCount();
            ViewBag.totalCommentCount = totalCommentCount;

            var allComments = await _commentService.GetAllCommentAsync(); // Tüm yorumları getir
            var recentComments = allComments.OrderByDescending(c => c.CreatedDate).Take(5).ToList(); // Son 5 yorumu al
            ViewBag.recentComments = recentComments;

            return View();
        }
    }
}   
