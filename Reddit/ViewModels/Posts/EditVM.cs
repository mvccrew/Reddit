using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Posts
{
    public class EditVM
    {
        public int Id { get; set; }

        public int SubRedditId { get; set; }

        public PostType PostType { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Post Title: ")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Content: ")]
        public string Content { get; set; }

        public EditVM()
        {

        }

        public EditVM(Post item)
        {
            Id = item.Id;
            SubRedditId = item.SubRedditId;
            PostType = item.PostType;
            Title = item.Title;
            Content = item.Content;
        }

        public void PopulateEntity(Post item)
        {
            item.Id = Id;
            item.SubRedditId = SubRedditId;
            item.PostType = PostType;
            item.Title = Title;
            item.Content = Content;
            item.CreationDate = DateTime.Now;
        }
    }
}