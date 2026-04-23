using Microsoft.AspNetCore.Mvc;
using StudentTaskTrackerMVC.Models;
using StudentTaskTrackerMVC.Services;

namespace StudentTaskTrackerMVC.Controllers
{
    public class QuotesController : Controller
    {
        private readonly ApiService _apiService;

        public QuotesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new QuoteViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(QuoteViewModel model)
        {
            var quote = await _apiService.GetRandomQuoteAsync();
            model.QuoteText = quote?.Text;
            return View(model);
        }
    }
}