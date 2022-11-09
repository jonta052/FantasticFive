﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class Article
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "The title is required!")]
        public string Title { get; set; } /*= String.Empty;*/
        
        [Required]
        public DateTime DateStamp { get; protected set; } = DateTime.Now;

        [Required]
        public string LinkText { get; set; }

        [Required]
        [DisplayName("Content Summary")]
        public string ContentSummary { get; set; }
       
        [Required]
        public string Content { get; set; }
        public int Views { get; set; }
       
        [Required]
        public string ImageLink { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public bool Archived { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public bool EditorChoice { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Dislike> Dislikes { get; set; }
    }
}
