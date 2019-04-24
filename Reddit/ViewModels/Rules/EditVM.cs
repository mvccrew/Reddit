using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reddit.ViewModels.Rules
{
    public class EditVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SubRedditId { get; set; }
        public EditVM()
        {
        }

        public EditVM(Rule item)
        {
            Id = item.Id;
            Text = item.Text;
            SubRedditId = item.SubRedditId;
        }

        public void PopulateEntity(Rule item)
        {
            item.Id = Id;
            item.Text = Text;
            item.SubRedditId = SubRedditId;
            item.CreationDate = DateTime.Now;
        }
    }
}