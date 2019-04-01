using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Comments
{
    public class EditVM
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public int? ParentCommentId { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Comment")]
        public string Text { get; set; }

        public EditVM()
        {
        }

        public EditVM(Comment item)
        {
            Id = item.Id;
            PostId = item.PostId;
            UserId = item.UserId;
            Text = item.Text;
            if (item.ParentCommentId != null)
                ParentCommentId = item.ParentCommentId;
        }

        public void PopulateEntity(Comment item)
        {
            item.Id = Id;
            item.PostId = PostId;
            item.UserId = UserId;
            item.Text = Text;
            if (ParentCommentId != null)
                item.ParentCommentId = ParentCommentId;
            item.CreationDate = DateTime.Now;
        }
    }
}