using hospitals.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor.RichTextEditor;
using System.Diagnostics;

namespace hospitals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    
        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The view result.</returns>
        public IActionResult Index()
        {
            return View();
        }
    
        /// <summary>
        /// Displays the privacy page.
        /// </summary>
        /// <returns>The view result.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Readme()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "README.md");
            string markdownContent = System.IO.File.ReadAllText(filePath);

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string htmlContent = Markdig.Markdown.ToHtml(markdownContent, pipeline);

            return Content(htmlContent, "text/html");
        }
    
        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The view result.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}