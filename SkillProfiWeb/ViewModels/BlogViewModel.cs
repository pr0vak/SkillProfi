using SkillProfi.DAL.Models;

namespace SkillProfiWeb.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public DateTime Created { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }



        public BlogViewModel() { }

        public BlogViewModel(Blog blog)
        {
            this.Id = blog.Id;
            this.Title = blog.Title;
            this.Description = blog.Description;
            this.ShortDescription = blog.ShortDescription;
            this.Created = blog.Created;
            this.ImageUrl = blog.ImageUrl;
        }
    }
}
