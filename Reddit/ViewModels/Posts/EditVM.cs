using DataAccess.Entities;
using Reddit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reddit.ViewModels.Posts
{
    public class EditVM
    {
        public int Id { get; set; }

        //[Required]
        [DisplayName("SubReddit: ")]
        public int SelectedSubReddit { get; set; }
        public List<SelectListItem> SubRedditsList { get; set; }
        public int SubRedditId { get; set; }

        public int UserId { get; set; }

        public DataAccess.Entities.SubReddit SubReddit { get; set; }

        public PostType PostType { get; set; }

        public int PostTypeId { get; set; }

        //[Required(ErrorMessage = "This field is required!")]
        [DisplayName("Post Title: ")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "This field is required!")]
        [DisplayName("Content: ")]
        public string Content { get; set; }

        public EditVM()
        {

        }

        public EditVM(Post item)
        {
            Id = item.Id;
            SelectedSubReddit = item.SubRedditId;
            SubRedditId = item.SubRedditId;
            UserId = item.UserId;
            //PostType = item.PostType;
            switch(item.PostType.ToString())
            {
                case "TextPost": PostTypeId = 1;break;
                case "VideoPost": PostTypeId = 3; break;
                case "ImagePost": PostTypeId = 2; break;
                default: PostTypeId = 0;break;
            }
            Title = item.Title;
            Content = item.Content;
        }

        public void PopulateEntity(Post item)
        {
            item.Id = Id;
            item.SubRedditId = SubRedditId;
            item.UserId = AuthenticationManager.LoggedUser.Id;
            switch(PostTypeId)
            {
                case 1: item.PostType = PostType.TextPost;break;
                case 2: item.PostType = PostType.ImagePost;break;
                case 3: item.PostType = PostType.VideoPost;break;
                default: item.PostType = PostType.Undefined;break;
            }
            item.Title = Title;
            item.Content = Content;
            item.CreationDate = DateTime.Now;
            item.IsApproved = false;
        }
    }
}