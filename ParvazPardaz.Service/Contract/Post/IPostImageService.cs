using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Post
{
    public interface IPostImageService : IBaseService<PostImage>
    {
        PostImage Upload(AddPostImageViewModel viewModel);
        PostImage UpoloadGallery(ImageSliderViewModel viewModel);
        bool Remove(int id);
        bool RemoveGallery(int id);
        }
}
