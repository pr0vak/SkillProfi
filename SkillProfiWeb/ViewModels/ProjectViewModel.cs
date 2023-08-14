using SkillProfi.DAL.Models;

namespace SkillProfiWeb.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }

        public ProjectViewModel() { }

        public ProjectViewModel(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.Description = project.Description;
            this.ImageUrl = project.ImageUrl;
        }
    }
}
