using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RedditApi.helpers;
using RedditApi.Models;

namespace RedditApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly RedditDbContext _context;
        public PostController(RedditDbContext context)
        {
            _context = context;
        }

        [HttpPost("post")]
        public async Task<ApiResponse<PostDTO>> addPost([FromBody] PostDTO post){
            if(post== null) {
                return new ApiResponse<PostDTO>
                {
                    status = "error",
                    error = "no data"
                };
            }
            else
            {
                var newPost =new Post() {Title = post.Title,Url=post.Url,SubredditId=post.SubredditId,UserId=post.UserId,Body=post.Body,
                CreatedAt=post.CreatedAt,UpdatedAt=post.UpdatedAt};
                await _context.Posts.AddAsync(newPost);
                await _context.SaveChangesAsync();
                return new ApiResponse<PostDTO> { status = "success" };
            }
        }

        [HttpPost("comment")]
        public async Task<ApiResponse<CommentDTO>> addComment([FromBody] CommentDTO comment)
        {
            if (comment == null)
            {
                return new ApiResponse<CommentDTO>
                {
                    status = "error",
                    error = "no data"
                };
            }
            else
            {
                var newComment = new Comment()
                {
                    UserId = comment.UserId,
                    PostId = comment.PostId,
                    Body = comment.Body,
                    CreatedAt = comment.CreatedAt,
                };
                await _context.Comments.AddAsync(newComment);
                await _context.SaveChangesAsync();
                return new ApiResponse<CommentDTO> { status = "success" };
            }
        }


        [HttpPost("subreddit")]
        public async Task<ApiResponse<CommentDTO>> addSubReddit([FromBody] SubRedditDTO subReddit)
        {
            if(subReddit == null)
            {
                return new ApiResponse<CommentDTO> { status="error",error="no data given" };
            }
            else
            {
                var newSubReddit = new Subreddit() 
                {
                    Id= subReddit.Id,
                    Title = subReddit.Title,
                    Description = subReddit.Description,
                    CreatedAt = subReddit.CreatedAt,
                };
                await _context.Subreddits.AddAsync(newSubReddit);
                await _context.SaveChangesAsync();
                return new ApiResponse<CommentDTO> { status="success" };
            }
        }

        [HttpGet("subreddits/{id:int}/posts")]
        public async Task<ApiResponse<List<PostDTO>>> getPostsBySubRedditId(int id)
        {
            if (id > 0)
            {
                var posts = await _context.Posts.Where(e => e.SubredditId == id).Select(e => new PostDTO()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Url = e.Url,
                    SubredditId = e.SubredditId,
                    UserId = e.UserId,
                    Body = e.Body,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                }).ToListAsync();

                if(posts.Count > 0)
                {
                    return new ApiResponse<List<PostDTO>>
                    {
                        status = "success",
                        data = posts,
                    };
                }
                else
                {
                    return new ApiResponse<List<PostDTO>> { status = "fail", error = "no data" };
                }
            }
            else
            {
                return new ApiResponse<List<PostDTO>>
                {
                    status = "error",
                    error = "invalid id"
                };
            }
        }

        [HttpGet("users/{id:int}/posts")]
        public async Task<ApiResponse<List<PostDTO>>> getPostsByUserId(int id)
        {
            if (id > 0)
            {
                var posts = await _context.Posts.Where(e => e.UserId == id).Select(e => new PostDTO()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Url = e.Url,
                    SubredditId = e.SubredditId,
                    UserId = e.UserId,
                    Body = e.Body,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                }).ToListAsync();

                if (posts.Count > 0)
                {
                    return new ApiResponse<List<PostDTO>>
                    {
                        status = "success",
                        data = posts,
                    };
                }
                else
                {
                    return new ApiResponse<List<PostDTO>> { status = "fail", error = "no data" };
                }
            }
            else
            {
                return new ApiResponse<List<PostDTO>>
                {
                    status = "error",
                    error = "invalid id"
                };
            }
        }

        [HttpGet("posts/{id:int}/comments")]
        public async Task<ApiResponse<List<CommentDTO>>> getCommentsByPostId(int id)
        {
            if (id > 0)
            {
                var comments = await _context.Comments.Where(e => e.PostId == id).Select(e => new CommentDTO() {
                    Id = e.Id,
                    UserId = e.UserId,
                    PostId = e.PostId,
                    Body = e.Body,
                    CreatedAt = e.CreatedAt,
                }).ToListAsync();
                if (comments.Count > 0)
                {
                    return new ApiResponse<List<CommentDTO>> { status = "success", data = comments };
                }
                else
                {
                    return new ApiResponse<List<CommentDTO>> { status = "fail", error = "no data" };
                }
            }
            else
            {
                return new ApiResponse<List<CommentDTO>> { status = "error", error = "failed" };
            }
        }

        [HttpGet("posts/all")]
        public async Task<ApiResponse<List<Postdetail>>> getPosts()
        {
            var posts = await _context.Postdetails.ToListAsync();
            return posts.Count > 0 ? new ApiResponse<List<Postdetail>> { status = "success", data = posts } :
                new ApiResponse<List<Postdetail>> { status = "fail", error = "no data" };
        }

        [HttpGet("subreddits/all")]
        public async Task<ApiResponse<List<SubRedditDTO>>> getSubReddits() 
        {
            var subreddits = await _context.Subreddits.Select(e => new SubRedditDTO()
            {
                Id= e.Id,
                Title = e.Title,
                Description = e.Description,
            }).ToListAsync();

            return subreddits.Count > 0 ? new ApiResponse<List<SubRedditDTO>> { status = "success", data = subreddits } :
                new ApiResponse<List<SubRedditDTO>> { status = "fail" };
        }
    
    }
}
