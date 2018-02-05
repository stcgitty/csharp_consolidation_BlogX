using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogX.Models;


namespace BlogX.data
{
    public class BlogRepository
    {
        static BlogRepository bloggy;
        public static BlogRepository GetRepository()
        {
            if(bloggy == null)
            {
                bloggy = new BlogRepository();
            }
            return bloggy;
        }

        public BlogRepository()
        {
            if (BlogSet == null)
            {
                BlogSet = new List<Blog>()
                {
                    new Blog()
                    {
                        Id = 1, Title = "This is a spiffy blog post",
                        Content = "This is some fantastic content for my spiffy blog post"
                    },
                    new Blog()
                    {
                        Id = 2, Title = "This is a second spiffy blog post",
                        Content = "This is some tolerable content for a 2nd blog post"
                    },
                    new Blog()
                    {
                        Id = 3, Title = "This is a 3rd spiffy blog post",
                        Content = "This is some fantastic content for my 3rd spiffy blog post"
                    },
                    new Blog()
                    {
                        Id = 4, Title = "This is a 4th spiffy blog post",
                        Content = "This is some tolerable content for a 4th blog post"
                    }
                };
            }

        }

        private static List<Blog> BlogSet;

        public Blog GetBlog(int id)
        {
            return BlogSet.SingleOrDefault(b => b.Id == id);
        }
        public List<Blog> GetBlogs()
        {
            return BlogSet;
        }
        public void SaveBlog(Blog blog)
        {
            BlogSet.Add(blog);
            blog.Id = WorkOutAnId();
        }
        public void Update(Blog blog)
        {
            Blog existingBlog = BlogSet.SingleOrDefault(b => b.Id == blog.Id);
            if(existingBlog == null)
            {
                throw new Exception("Cannae find it.");
            } else
            {
                BlogSet.Remove(existingBlog);
                BlogSet.Add(blog);
            }
            

        }
        private int WorkOutAnId()
        {
            int largest = BlogSet.Max(b => b.Id);
            return largest+1;
        }
        public int GetBlogCount()
        {
            return this.GetBlogs().Count;
        }



    }
}