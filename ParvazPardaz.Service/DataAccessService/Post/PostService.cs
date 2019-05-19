using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ParvazPardaz.Common.Extension;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;
using Newtonsoft.Json;
using ParvazPardaz.Common.Utility;
using EntityComment = ParvazPardaz.Model.Entity.Comment.Comment;
using EntityPost = ParvazPardaz.Model.Entity.Post.Post;


namespace ParvazPardaz.Service.DataAccessService.Post
{
    public class PostService : BaseService<ParvazPardaz.Model.Entity.Post.Post>, IPostService
    {
        #region Fields
        private const int Width50 = 50;
        private const int Height50 = 50;
        private const int A5Width = 100;
        private const int A5Height = 100;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/Post/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ParvazPardaz.Model.Entity.Post.Post> _dbSet;
        private readonly IDbSet<ParvazPardaz.Model.Entity.Hotel.Hotel> _dbSetHotel;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public PostService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<ParvazPardaz.Model.Entity.Post.Post>();
            _dbSetHotel = _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.Hotel>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridPostViewModel> GetViewModelForGrid(string username)
        {
            if (username != "")
            {
                //int _userId = System.Convert.ToInt32(username);
                return _dbSet.Where(w => w.IsDeleted == false && w.CreatorUser.UserName == username).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridPostViewModel>(_mappingEngine);
            }
            else
                return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                            .AsNoTracking().ProjectTo<GridPostViewModel>(_mappingEngine);
            //return _dbSet.Join(_unitOfWork.Set<LinkTable>(), p => p.Id, l => l.typeId, (p, l) => new { p, l })
            //       .Where(w => w.p.IsDeleted == false).Include(x=>x.l.URL).Include(i => i.p.CreatorUser).Include(i => i.p.ModifierUser)
            //                                             .AsNoTracking().ProjectTo<GridPostViewModel>(_mappingEngine);
        }
        #endregion

        #region GetTagsForDDL
        public IEnumerable<SelectListItem> GetTagsForDDL()
        {
            return _unitOfWork.Set<Tag>().Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Name, Value = x.Name }).AsEnumerable();
        }
        #endregion

        #region Create
        public async Task<AddPostViewModel> CreateAsync(AddPostViewModel viewModel)
        {
            var model = _mappingEngine.Map<ParvazPardaz.Model.Entity.Post.Post>(viewModel);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageUrl = relativePathWithFileNameOriginal;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageFileName = viewModel.File.FileName;
                model.ImageSize = viewModel.File.ContentLength;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFileExact(Width50, Height50);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }

            PostGroup _postGroup = new PostGroup();
            model.PostGroups = new List<PostGroup>();
            if (viewModel._selectedPostGroups == null)
            {
                PostGroup postGroup = _unitOfWork.Set<PostGroup>().Where(x => x.Name == "دسته بندی نشده").FirstOrDefault();
                model.PostGroups.Add(postGroup);
            }
            else
            {
                foreach (var item in viewModel._selectedPostGroups)
                {
                    _postGroup = _unitOfWork.Set<PostGroup>().Find(item);
                    model.PostGroups.Add(_postGroup);
                }
            }

            #region Saving Tags
            if (viewModel.TagTitles != null && viewModel.TagTitles.Any())
            {
                //model.Tags = new List<ProductTagProduct>();
                List<Tag> tagsInDb = new List<Tag>();
                foreach (var tagTitle in viewModel.TagTitles.ToList())
                {
                    model.Tags = new List<Tag>();
                    var tagInDB = _unitOfWork.Set<Tag>().FirstOrDefault(x => x.Name == tagTitle);
                    if (tagInDB == null)
                    {
                        var newTag = new Tag() { Name = tagTitle };

                        //save tag in productTag table
                        _unitOfWork.Set<Tag>().Add(newTag);


                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = insertedTag.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(newTag);
                        //model.Tags.Add(newTag);
                    }
                    else
                    {
                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = tagInDB.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(tagInDB);
                        //model.Tags.Add(tagInDB);
                    }
                }
                model.Tags = tagsInDb;

            }
            #endregion

            _dbSet.Add(model);
            await _unitOfWork.SaveAllChangesAsync();
            return await _dbSet.OrderByDescending(x => x.Id).AsNoTracking().ProjectTo<AddPostViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region Edit
        public async Task<int> EditAsync(EditPostViewModel viewModel)
        {
            var model = await base.GetByIdAsync(c => c.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, model);

            #region Update image
            if (viewModel.File.HasFile())
            {
                //حذف تصویر بزرگ قبلی
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl));
                }
                //حذف تصویر کوچک قبلی
                var splittedUrl = model.ImageUrl.Split('.');
                var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
                }
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageUrl = relativePathWithFileNameOriginal;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageFileName = viewModel.File.FileName;
                model.ImageSize = viewModel.File.ContentLength;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFileExact(Width50, Height50);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            #endregion

            #region Update postGroup
            PostGroup _postGroup = new PostGroup();
            List<PostGroup> postGroups = model.PostGroups.ToList();

            //if (!viewModel._selectedPostGroups.Equals(postGroups.Select(x => x.Id)))
            //{
            foreach (var item in postGroups)
            {
                model.PostGroups.Remove(item);
            }
            if (viewModel._selectedPostGroups == null)
            {
                PostGroup postGroup = _unitOfWork.Set<PostGroup>().Where(x => x.Name == "دسته بندی نشده").FirstOrDefault();
                model.PostGroups.Add(postGroup);
            }
            else
            {
                foreach (var item in viewModel._selectedPostGroups)
                {
                    _postGroup = _unitOfWork.Set<PostGroup>().Find(item);
                    model.PostGroups.Add(_postGroup);
                }
            }
            //} 
            #endregion

            #region remove previous Tags
            List<Tag> tagList = model.Tags.ToList();
            if (tagList != null && tagList.Any())
            {
                foreach (var t in tagList)
                {
                    model.Tags.Remove(t);
                }
            }
            //else
            //{
            //    model.Tags = new List<Tag>();
            //}
            #endregion

            #region Saving Tags
            if (viewModel.TagTitles != null && viewModel.TagTitles.Any())
            {
                //model.Tags = new List<ProductTagProduct>();
                List<Tag> tagsInDb = new List<Tag>();
                foreach (var tagTitle in viewModel.TagTitles.ToList())
                {
                    model.Tags = new List<Tag>();
                    var tagInDB = _unitOfWork.Set<Tag>().FirstOrDefault(x => x.Name == tagTitle);
                    if (tagInDB == null)
                    {
                        var newTag = new Tag() { Name = tagTitle };

                        //save tag in productTag table
                        _unitOfWork.Set<Tag>().Add(newTag);


                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = insertedTag.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(newTag);
                        //model.Tags.Add(newTag);
                    }
                    else
                    {
                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = tagInDB.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(tagInDB);
                        //model.Tags.Add(tagInDB);
                    }
                }

                model.Tags = tagsInDb;
            }
            #endregion

            await _unitOfWork.SaveAllChangesAsync();
            return viewModel.Id;
            //await _unitOfWork.SaveAllChangesAsync();
            //return await _dbSet
            //return await _dbSet.OrderByDescending(x => x.Id).AsNoTracking().ProjectTo<EditPostViewModel>(_mappingEngine).FirstOrDefaultAsync();

        }
        #endregion

        #region GetAllCountryOfSelectListItem
        public IEnumerable<SelectListItem> GetAllCountryOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Name, Value = x.Id.ToString() }).AsEnumerable();
        }
        #endregion

        //#region GetAllCountryOfSelectListItem
        //public IEnumerable<SelectListItem> GetAllNationalitiesOfSelectListItem()
        //{
        //    return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Nationality, Value = x.Id.ToString() }).AsEnumerable();
        //}
        //#endregion

        #region GetCitiesByCountry
        //public IEnumerable<SelectListItem> GetCitiesByCountry(int countryId)
        //{
        //    return
        //        _dbSet.AsNoTracking()
        //                       .Include(i => i.Cities)
        //                       .FirstOrDefault(c => c.Id == countryId).Cities
        //                       .Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() });
        //}
        #endregion

        #region joinPostToLinkTbl
        public List<PostDetailViewModel> JoinToLink(List<ParvazPardaz.Model.Entity.Post.Post> listModel)
        {
            List<PostDetailViewModel> vm = new List<PostDetailViewModel>();
            if (listModel != null)
            {
                vm = (from p in listModel
                      join l in _unitOfWork.Set<LinkTable>().ToList()
                          on p.Id equals l.typeId
                      where l.linkType == LinkType.Post && l.Visible == true && l.IsDeleted == false
                      select new PostDetailViewModel
                      {
                          AccessLevel = p.AccessLevel,
                          ExpireDatetime = p.ExpireDatetime,
                          Id = p.Id,
                          IsActiveComments = p.IsActiveComments,
                          PostContent = p.PostContent,
                          PostRateAvg = p.PostRateAvg,
                          PostRateCount = p.PostRateCount,
                          PostSort = p.PostSort,
                          PostSummery = p.PostSummery,
                          ModifierDateTime = p.ModifierDateTime.Value,
                          PublishDatetime = p.PublishDatetime,
                          VisitCount = l.VisitCount,
                          PostGroups = (from pg in p.PostGroups
                                        join lg in _unitOfWork.Set<LinkTable>().ToList()
                                            on pg.Id equals lg.typeId
                                        where lg.linkType == LinkType.PostGroup
                                        select new PostGroupViewModel
                                        {
                                            Name = lg.Name,
                                            Rel = lg.Rel,
                                            Target = lg.Target,
                                            Title = lg.Title,
                                            URL = lg.URL,

                                        }).ToList(),
                          PostImages = p.PostImages,
                          //PostTags = (from tg in p.Tags
                          //            join lg in _unitOfWork.Set<LinkTable>().ToList()
                          //                on tg.Id equals lg.typeId
                          //            where lg.linkType == LinkType.PostTag
                          //            select new PostGroupViewModel
                          //            {
                          //                Name = lg.Name,
                          //                Rel = lg.Rel,
                          //                Target = lg.Target,
                          //                Title = lg.Title,
                          //                URL = lg.URL,

                          //            }).ToList(),

                          // ImageUrl = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl + "-736x512" + p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension,

                          //ImageUrl765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl,
                          //Extention765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension,
                          //Name765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageFileName,

                          //ImageUrl575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageUrl,
                          //Extention575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageExtension,
                          //Name575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageFileName,

                          //ImageUrl370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageUrl,
                          //Extention370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageExtension,
                          //Name370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageFileName,

                          //ImageUrl277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageUrl,
                          //Extention277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageExtension,
                          //Name277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageFileName,

                          //ImageUrl98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageUrl,
                          //Extention98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageExtension,
                          //Name98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageFileName,

                          MetaDescription = l.Description,
                          MetaKeywords = l.Keywords,
                          Name = l.Name,
                          Rel = l.Rel,
                          Target = l.Target,
                          Title = l.Title,
                          URL = l.URL,
                          LikeCount = p.LikeCount == null ? 0 : p.LikeCount
                      }).ToList();
                foreach (var item in vm)
                {
                    if (item.PostImages != null)
                    {
                       // item.ImageUrl = item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault()!=null?item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl + "-736x512" + p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension:
                         
                        item.ImageUrl765x535 = item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl:"#";
                        item.Extention765x535 = item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension:"#";
                        item.ImageUrl = item.ImageUrl765x535 + "-736x512"+ item.Extention765x535;
                        item.Name765x535 = item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault()!=null?item.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageFileName:"#";
                        item.ImageUrl575x530 = item.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageUrl:"#";
                        item.Extention575x530 = item.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageExtension:"#";
                        item.Name575x530 = item.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault()!=null?item.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageFileName:"#";
                        item.ImageUrl370x292 = item.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageUrl:"#";
                        item.Extention370x292 = item.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault()!=null?item.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageExtension:"#";
                        item.Name370x292 = item.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault()!=null?item.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageFileName:"#";
                        item.ImageUrl277x186 = item.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageUrl:"#";
                        item.Extention277x186 = item.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageExtension:"#";
                        item.Name277x186 = item.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageFileName:"#";
                        item.ImageUrl98x98 = item.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageUrl:"#";
                        item.Extention98x98 = item.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageExtension:"#";
                        item.Name98x98 = item.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault()!=null? item.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageFileName:"#";
                    }
                }
            }
            return vm;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public List<PostDetailViewModel> JoinHotelToLink(List<ParvazPardaz.Model.Entity.Post.Post> listModel)
        {
            List<PostDetailViewModel> vm = new List<PostDetailViewModel>();
            vm = (from p in listModel
                  join l in _unitOfWork.Set<LinkTable>().ToList()
                      on p.Id equals l.typeId
                  where l.linkType == LinkType.Post && l.Visible == true && l.IsDeleted == false
                  select new PostDetailViewModel
                  {
                      AccessLevel = p.AccessLevel,
                      ExpireDatetime = p.ExpireDatetime,
                      Id = p.Id,
                      IsActiveComments = p.IsActiveComments,
                      PostContent = p.PostContent,
                      PostRateAvg = p.PostRateAvg,
                      PostRateCount = p.PostRateCount,
                      PostSort = p.PostSort,
                      PostSummery = p.PostSummery,
                      PublishDatetime = p.PublishDatetime,
                      VisitCount = l.VisitCount,
                      PostGroups = (from pg in p.PostGroups
                                    join lg in _unitOfWork.Set<LinkTable>().ToList()
                                        on pg.Id equals lg.typeId
                                    where lg.linkType == LinkType.PostGroup
                                    select new PostGroupViewModel
                                    {
                                        Name = lg.Name,
                                        Rel = lg.Rel,
                                        Target = lg.Target,
                                        Title = lg.Title,
                                        URL = lg.URL,

                                    }).ToList(),
                      //PostTags = (from tg in p.Tags
                      //            join lg in _unitOfWork.Set<LinkTable>().ToList()
                      //                on tg.Id equals lg.typeId
                      //            where lg.linkType == LinkType.PostTag
                      //            select new PostGroupViewModel
                      //            {
                      //                Name = lg.Name,
                      //                Rel = lg.Rel,
                      //                Target = lg.Target,
                      //                Title = lg.Title,
                      //                URL = lg.URL,

                      //            }).ToList(),

                      ImageUrl = p.PostImages.FirstOrDefault().ImageUrl + "-261x177" + p.PostImages.FirstOrDefault().ImageExtension,
                      Name575x530 = p.PostImages.FirstOrDefault().ImageFileName,
                      Extention575x530 = p.PostImages.FirstOrDefault().ImageExtension,
                      //ImageUrl765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl,
                      //Extention765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension,
                      //Name765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageFileName,

                      //ImageUrl575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageUrl,
                      //Extention575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageExtension,
                      //Name575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageFileName,

                      //ImageUrl370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageUrl,
                      //Extention370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageExtension,
                      //Name370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageFileName,

                      //ImageUrl277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageUrl,
                      //Extention277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageExtension,
                      //Name277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageFileName,

                      //ImageUrl98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageUrl,
                      //Extention98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageExtension,
                      //Name98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageFileName,

                      MetaDescription = l.Description,
                      MetaKeywords = l.Keywords,
                      Name = l.Name,
                      Rel = l.Rel,
                      Target = l.Target,
                      Title = l.Title,
                      URL = l.URL

                  }

                                ).ToList();
            return vm;
        }

        /// <summary>
        /// Get last 20 Related post by tags
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public IList<PostListDetailViewModel> RelatedPost(List<string> tags)
        {
            List<PostListDetailViewModel> posts = (from p in _dbSet
                                                   join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                   on p.Id equals l.typeId
                                                   where p.Tags.Any(x => tags.Contains(x.Name)) && p.IsDeleted.Equals(false) && l.Visible == true && l.linkType == LinkType.Post /*&& p.PostGroups.Any(c=>!c.Title.Contains("introducing-hotels"))*/
                                                   orderby p.PublishDatetime descending
                                                   select new PostListDetailViewModel
                                                   {
                                                       Id = p.Id,
                                                       PostRateAvg = p.PostRateAvg,
                                                       PostRateCount = p.PostRateCount,
                                                       PostSummery = p.PostSummery,
                                                       PublishDatetime = p.PublishDatetime,
                                                       VisitCount = l.VisitCount,
                                                       Image = p.PostImages.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                       {
                                                           ImageUrl = x.ImageUrl,
                                                           Height = x.Height,
                                                           Id = x.Id,
                                                           ImageDesc = x.ImageDesc,
                                                           ImageExtension = x.ImageExtension,
                                                           ImageFileName = x.ImageFileName,
                                                           ImageTitle = x.ImageTitle,
                                                           IsPrimarySlider = x.IsPrimarySlider,
                                                           Width = x.Width
                                                       }).FirstOrDefault(),
                                                       Name = l.Name,
                                                       Rel = l.Rel,
                                                       Target = l.Target,
                                                       Title = l.Title,
                                                       URL = l.URL
                                                   }).ToList();
            return posts;

        }
        #endregion

        #region LastUpdated
        public IList<PostListDetailViewModel> LastUpdated(int count)
        {
            List<PostListDetailViewModel> posts = (from p in _dbSet
                                                   join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                   on p.Id equals l.typeId
                                                   where l.linkType == LinkType.Post && p.IsDeleted.Equals(false) && l.Visible == true
                                                   orderby p.PublishDatetime descending
                                                   select new PostListDetailViewModel
                                                   {
                                                       Id = p.Id,
                                                       PostRateAvg = p.PostRateAvg,
                                                       PostRateCount = p.PostRateCount,
                                                       PostSummery = p.PostSummery,
                                                       PublishDatetime = p.PublishDatetime,
                                                       VisitCount = l.VisitCount,
                                                       Image = p.PostImages.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                       {
                                                           ImageUrl = x.ImageUrl,
                                                           Height = x.Height,
                                                           Id = x.Id,
                                                           ImageDesc = x.ImageDesc,
                                                           ImageExtension = x.ImageExtension,
                                                           ImageFileName = x.ImageFileName,
                                                           ImageTitle = x.ImageTitle,
                                                           IsPrimarySlider = x.IsPrimarySlider,
                                                           Width = x.Width
                                                       }).FirstOrDefault(),
                                                       Name = l.Name,
                                                       Rel = l.Rel,
                                                       Target = l.Target,
                                                       Title = l.Title,
                                                       URL = l.URL,
                                                       LikeCount = p.LikeCount == null ? 0 : p.LikeCount
                                                   }).Take(count).ToList();
            return posts;
        }
        #endregion

        #region LatestPost
        public IList<PostListDetailViewModel> LatestPost()
        {
            List<PostListDetailViewModel> posts = (from p in _dbSet
                                                   join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                   on p.Id equals l.typeId
                                                   where l.linkType == LinkType.Post && l.Visible == true && p.IsActive == true && p.IsDeleted == false && l.IsDeleted == false
                                                   orderby p.PublishDatetime descending
                                                   select new PostListDetailViewModel
                                                   {
                                                       Id = p.Id,
                                                       PostRateAvg = p.PostRateAvg,
                                                       PostRateCount = p.PostRateCount,
                                                       PostSummery = p.PostSummery,
                                                       PublishDatetime = p.PublishDatetime,
                                                       VisitCount = l.VisitCount,
                                                       Image = p.PostImages.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                       {
                                                           ImageUrl = x.ImageUrl,
                                                           Height = x.Height,
                                                           Id = x.Id,
                                                           ImageDesc = x.ImageDesc,
                                                           ImageExtension = x.ImageExtension,
                                                           ImageFileName = x.ImageFileName,
                                                           ImageTitle = x.ImageTitle,
                                                           IsPrimarySlider = x.IsPrimarySlider,
                                                           Width = x.Width
                                                       }).FirstOrDefault(),
                                                       Name = l.Name,
                                                       Rel = l.Rel,
                                                       Target = l.Target,
                                                       Title = l.Title,
                                                       URL = l.URL,

                                                   }).ToList();
            return posts;
        }
        #endregion

        #region RelatedPost
        public IList<PostListDetailViewModel> RelatedPost(List<string> tags, int currentPostId)
        {
            List<PostListDetailViewModel> posts = (from p in _dbSet
                                                   join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                   on p.Id equals l.typeId
                                                   where p.Tags.Any(x => tags.Contains(x.Name)) && p.IsDeleted.Equals(false) && l.Visible == true && l.linkType == LinkType.Post && l.typeId != currentPostId /*&& p.PostGroups.Any(c=>!c.Title.Contains("introducing-hotels"))*/
                                                   orderby p.PublishDatetime descending
                                                   select new PostListDetailViewModel
                                                   {
                                                       Id = p.Id,
                                                       PostRateAvg = p.PostRateAvg,
                                                       PostRateCount = p.PostRateCount,
                                                       PostSummery = p.PostSummery,
                                                       PublishDatetime = p.PublishDatetime,
                                                       VisitCount = l.VisitCount,
                                                       Image = p.PostImages.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                       {
                                                           ImageUrl = x.ImageUrl,
                                                           Height = x.Height,
                                                           Id = x.Id,
                                                           ImageDesc = x.ImageDesc,
                                                           ImageExtension = x.ImageExtension,
                                                           ImageFileName = x.ImageFileName,
                                                           ImageTitle = x.ImageTitle,
                                                           IsPrimarySlider = x.IsPrimarySlider,
                                                           Width = x.Width
                                                       }).FirstOrDefault(),
                                                       Name = l.Name,
                                                       Rel = l.Rel,
                                                       Target = l.Target,
                                                       Title = l.Title,
                                                       URL = l.URL,

                                                   }).ToList();
            return posts;
        }
        #endregion

        #region PostLists + PostListsOrderBy
        public IList<PostListDetailViewModel> PostLists(int id, LinkType linktype, out bool hasHotel)
        {
            bool hasAnyHotel = false;

            List<PostListDetailViewModel> items = new List<PostListDetailViewModel>();
            if (linktype == LinkType.PostGroup)
            {
                // var linkTypes= (from l in _unitOfWork.Set<LinkTable>() where l.linkType==LinkType.PostTag select l).ToList();
                items = (from d in _dbSet
                         join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                         on d.Id equals l.typeId
                         where l.linkType == LinkType.Post && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && d.PostGroups.Any(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                         orderby d.CreatorDateTime descending
                         select new PostListDetailViewModel
                         {
                             Id = d.Id,
                             Image = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                             {
                                 ImageUrl = x.ImageUrl,
                                 Height = x.Height,
                                 Id = x.Id,
                                 ImageDesc = x.ImageDesc,
                                 ImageExtension = x.ImageExtension,
                                 ImageFileName = x.ImageFileName,
                                 ImageTitle = x.ImageTitle,
                                 IsPrimarySlider = x.IsPrimarySlider,
                                 Width = x.Width
                             }).FirstOrDefault(),
                             Thumbnail = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageUrl + "-765x535" + d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageExtension,
                             Name = d.Name,
                             PostRateAvg = d.PostRateAvg,
                             PostRateCount = d.PostRateCount,
                             PostSummery = d.PostSummery,
                             PublishDatetime = d.PublishDatetime,
                             VisitCount = l.VisitCount,
                             Rel = l.Rel,
                             Target = l.Target,
                             URL = l.URL,
                             Title = l.Title,
                             //PostGroups = (from pg in d.PostGroups
                             //              join lg in _unitOfWork.Set<LinkTable>().AsEnumerable()
                             //                  on pg.Id equals lg.typeId
                             //              where lg.linkType == LinkType.PostGroup
                             //              select new PostGroupViewModel
                             //              {
                             //                  Name = lg.Name,
                             //                  Rel = lg.Rel,
                             //                  Target = lg.Target,
                             //                  Title = lg.Title,
                             //                  URL = lg.URL
                             //              }).ToList(),
                         }).ToList();

                var hotels = (from d in _dbSetHotel
                              join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                              on d.Id equals l.typeId
                              where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && d.PostGroups.Any(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                              orderby d.CreatorDateTime descending
                              select new PostListDetailViewModel
                              {
                                  Id = d.Id,

                                  Thumbnail = d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? d.HotelGalleries.FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.FirstOrDefault().ImageExtension : d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension,
                                  //Image = d.HotelGalleries.Where(x => x.Width == 765 || x.Width == 700 && x.IsPrimarySlider==true && x.IsDeleted==false).Select(x => new ImageViewModel
                                  //{
                                  //    ImageUrl = x.ImageUrl,
                                  //    Height = x.Height,
                                  //    Id = x.Id,
                                  //    ImageDesc = x.ImageDesc,
                                  //    ImageExtension = x.ImageExtension,
                                  //    ImageFileName = x.ImageFileName,
                                  //    ImageTitle = x.ImageTitle,
                                  //    IsPrimarySlider = x.IsPrimarySlider,
                                  //    Width = x.Width
                                  //}).FirstOrDefault(),
                                  Name = d.Title,
                                  PostRateAvg = d.PostRateAvg,
                                  PostRateCount = d.PostRateCount,
                                  PostSummery = d.Summary,
                                  PublishDatetime = d.PublishDatetime,
                                  VisitCount = l.VisitCount,
                                  Rel = l.Rel,
                                  Target = l.Target,
                                  URL = l.URL,
                                  Title = l.Title,
                                  //PostGroups = (from pg in d.PostGroups
                                  //              join lg in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                  //                  on pg.Id equals lg.typeId
                                  //              where lg.linkType == LinkType.PostGroup
                                  //              select new PostGroupViewModel
                                  //              {
                                  //                  Name = lg.Name,
                                  //                  Rel = lg.Rel,
                                  //                  Target = lg.Target,
                                  //                  Title = lg.Title,
                                  //                  URL = lg.URL
                                  //              }).ToList(),
                              }).ToList();
                if (hotels != null && hotels.Any())
                {
                    hasAnyHotel = true;
                }
                items.AddRange(hotels);
            }
            else if (linktype == LinkType.PostTag)
            {
                items = (from d in _dbSet
                         join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                         on d.Id equals l.typeId
                         where l.linkType == LinkType.Post && l.IsDeleted == false && d.Tags.Any(x => x.Id == id && x.IsDeleted == false)
                         select new PostListDetailViewModel
                         {
                             Id = d.Id,
                             Image = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                             {
                                 ImageUrl = x.ImageUrl,
                                 Height = x.Height,
                                 Id = x.Id,
                                 ImageDesc = x.ImageDesc,
                                 ImageExtension = x.ImageExtension,
                                 ImageFileName = x.ImageFileName,
                                 ImageTitle = x.ImageTitle,
                                 IsPrimarySlider = x.IsPrimarySlider,
                                 Width = x.Width
                             }).FirstOrDefault(),
                             Thumbnail = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageUrl + "-765x535" + d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageExtension,
                             Name = l.Title,
                             PostRateAvg = d.PostRateAvg,
                             PostRateCount = d.PostRateCount,
                             PostSummery = d.PostSummery,
                             PublishDatetime = d.PublishDatetime,
                             VisitCount = l.VisitCount,
                             Rel = l.Rel,
                             Target = l.Target,
                             URL = l.URL,
                             Title = l.Title,
                             //PostGroups = (from pg in d.PostGroups
                             //              join lg in _unitOfWork.Set<LinkTable>().AsQueryable()
                             //                  on pg.Id equals lg.typeId
                             //              where lg.linkType == LinkType.PostGroup
                             //              select new PostGroupViewModel
                             //              {
                             //                  Name = lg.Name,
                             //                  Rel = lg.Rel,
                             //                  Target = lg.Target,
                             //                  Title = lg.Title,
                             //                  URL = lg.URL,

                             //              }).ToList(),
                         }).ToList();

                var hotels = (from d in _dbSetHotel
                              join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                              on d.Id equals l.typeId
                              where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && d.Tags.Any(x => x.Id == id && x.IsDeleted == false) //&& d.PostGroups.Any(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                              orderby d.CreatorDateTime descending
                              select new PostListDetailViewModel
                              {
                                  Id = d.Id,
                                  Image = d.HotelGalleries.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                                  {
                                      ImageUrl = x.ImageUrl,
                                      Height = x.Height,
                                      Id = x.Id,
                                      ImageDesc = x.ImageDesc,
                                      ImageExtension = x.ImageExtension,
                                      ImageFileName = x.ImageFileName,
                                      ImageTitle = x.ImageTitle,
                                      IsPrimarySlider = x.IsPrimarySlider,
                                      Width = x.Width
                                  }).FirstOrDefault(),
                                  Thumbnail = d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? d.HotelGalleries.FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.FirstOrDefault().ImageExtension : d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension,
                                  Name = d.Title,
                                  PostRateAvg = d.PostRateAvg,
                                  PostRateCount = d.PostRateCount,
                                  PostSummery = d.Summary,
                                  PublishDatetime = d.PublishDatetime,
                                  VisitCount = l.VisitCount,
                                  Rel = l.Rel,
                                  Target = l.Target,
                                  URL = l.URL,
                                  Title = l.Title,
                                  //PostGroups = (from pg in d.PostGroups
                                  //              join lg in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                  //                  on pg.Id equals lg.typeId
                                  //              where lg.linkType == LinkType.PostGroup
                                  //              select new PostGroupViewModel
                                  //              {
                                  //                  Name = lg.Name,
                                  //                  Rel = lg.Rel,
                                  //                  Target = lg.Target,
                                  //                  Title = lg.Title,
                                  //                  URL = lg.URL
                                  //              }).ToList(),
                              }).ToList();
                if (hotels != null && hotels.Any())
                {
                    hasAnyHotel = true;
                }
                items.AddRange(hotels);
            }
            hasHotel = hasAnyHotel;
            return items;
        }

        public IList<PostListDetailViewModel> PostLists(int id, LinkType linktype, string orby, out bool hasHotel)
        {
            bool hasAnyHotel = false;

            List<PostListDetailViewModel> items = new List<PostListDetailViewModel>();
            if (linktype == LinkType.PostGroup)
            {
                #region post's order by
                var allPosts = new List<EntityPost>();
                switch (orby)
                {
                    case "mostrate":
                        allPosts = _dbSet.OrderByDescending(x => x.PostRateAvg).ToList();
                        break;

                    case "mostlike":
                        allPosts = _dbSet.OrderByDescending(x => x.LikeCount).ToList();
                        break;

                    case "alphabet":
                        allPosts = _dbSet.OrderBy(x => x.Name).ToList();
                        break;

                    default:
                        allPosts = _dbSet.ToList();
                        break;
                }
                #endregion
                #region posts
                items = (from d in allPosts
                         join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                         on d.Id equals l.typeId
                         where l.linkType == LinkType.Post && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive && d.PostGroups.Any(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                         select new PostListDetailViewModel
                         {
                             Id = d.Id,
                             Image = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                             {
                                 ImageUrl = x.ImageUrl,
                                 Height = x.Height,
                                 Id = x.Id,
                                 ImageDesc = x.ImageDesc,
                                 ImageExtension = x.ImageExtension,
                                 ImageFileName = x.ImageFileName,
                                 ImageTitle = x.ImageTitle,
                                 IsPrimarySlider = x.IsPrimarySlider,
                                 Width = x.Width

                             }).FirstOrDefault(),
                             Thumbnail = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageUrl + "-765x535" + d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageExtension,
                             Name = d.Name,
                             PostRateAvg = d.PostRateAvg,
                             PostRateCount = d.PostRateCount,
                             PostSummery = d.PostSummery,
                             PublishDatetime = d.PublishDatetime,
                             VisitCount = l.VisitCount,
                             Rel = l.Rel,
                             Target = l.Target,
                             URL = l.URL,
                             Title = l.Title,

                         }).ToList();
                #endregion

                #region hotel order by
                var allhotels = new List<ParvazPardaz.Model.Entity.Hotel.Hotel>();
                switch (orby)
                {
                    case "mostrate":
                        allhotels = _dbSetHotel.OrderByDescending(x => x.PostRateAvg).ToList();
                        break;

                    case "mosthotelrank":
                        allhotels = _dbSetHotel.OrderBy(x => x.HotelRank.OrderId).ToList();
                        break;

                    case "alphabet":
                        allhotels = _dbSetHotel.OrderBy(x => x.Title.Trim()).ToList();
                        break;

                    case "mostreviewcount":
                        allhotels = _dbSetHotel.GroupJoin(_unitOfWork.Set<EntityComment>().Where(w => w.IsApproved && !w.IsDeleted), jH => jH.Id, jC => jC.OwnId, (jH, jC) => new { jH, jC })
                            .SelectMany(sm => sm.jC.DefaultIfEmpty(), (sm, c) => new { sm.jH, c })
                            .GroupBy(g => new { g.jH, g.c })
                            .Select(s => new { hotel = s.Key.jH, commentCount = s.Count() })
                            .OrderByDescending(o => o.commentCount)
                            .Distinct()
                            .Select(h => h.hotel)
                            .ToList();
                        break;

                    default:
                        allhotels = _dbSetHotel.ToList();
                        break;
                }
                #endregion
                #region hotels
                var hotels = (from d in allhotels
                              join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                              on d.Id equals l.typeId
                              where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && d.PostGroups.Any(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                              select new PostListDetailViewModel
                              {
                                  Id = d.Id,
                                  Thumbnail = d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? d.HotelGalleries.FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.FirstOrDefault().ImageExtension : d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension,
                                  Name = d.Title,
                                  PostRateAvg = d.PostRateAvg,
                                  PostRateCount = d.PostRateCount,
                                  PostSummery = d.Summary,
                                  PublishDatetime = d.PublishDatetime,
                                  VisitCount = l.VisitCount,
                                  Rel = l.Rel,
                                  Target = l.Target,
                                  URL = l.URL,
                                  Title = l.Title,
                              }).ToList();

                if (hotels != null && hotels.Any())
                {
                    hasAnyHotel = true;
                }
                items.AddRange(hotels);
                #endregion
            }
            else if (linktype == LinkType.PostTag)
            {
                #region post's order by
                var allPosts = new List<EntityPost>();
                switch (orby)
                {
                    case "mostrate":
                        allPosts = _dbSet.OrderByDescending(x => x.PostRateAvg).ToList();
                        break;

                    case "mostlike":
                        allPosts = _dbSet.OrderByDescending(x => x.LikeCount).ToList();
                        break;

                    case "alphabet":
                        allPosts = _dbSet.OrderBy(x => x.Name).ToList();
                        break;

                    default:
                        allPosts = _dbSet.ToList();
                        break;
                }
                #endregion
                #region posts
                items = (from d in allPosts
                         join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                         on d.Id equals l.typeId
                         where l.linkType == LinkType.Post && l.IsDeleted == false && d.Tags.Any(x => x.Id == id && x.IsDeleted == false)
                         select new PostListDetailViewModel
                         {
                             Id = d.Id,
                             Image = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                             {
                                 ImageUrl = x.ImageUrl,
                                 Height = x.Height,
                                 Id = x.Id,
                                 ImageDesc = x.ImageDesc,
                                 ImageExtension = x.ImageExtension,
                                 ImageFileName = x.ImageFileName,
                                 ImageTitle = x.ImageTitle,
                                 IsPrimarySlider = x.IsPrimarySlider,
                                 Width = x.Width
                             }).FirstOrDefault(),
                             Thumbnail = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageUrl + "-765x535" + d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageExtension,
                             Name = l.Title,
                             PostRateAvg = d.PostRateAvg,
                             PostRateCount = d.PostRateCount,
                             PostSummery = d.PostSummery,
                             PublishDatetime = d.PublishDatetime,
                             VisitCount = l.VisitCount,
                             Rel = l.Rel,
                             Target = l.Target,
                             URL = l.URL,
                             Title = l.Title,
                             //PostGroups = (from pg in d.PostGroups
                             //              join lg in _unitOfWork.Set<LinkTable>().AsQueryable()
                             //                  on pg.Id equals lg.typeId
                             //              where lg.linkType == LinkType.PostGroup
                             //              select new PostGroupViewModel
                             //              {
                             //                  Name = lg.Name,
                             //                  Rel = lg.Rel,
                             //                  Target = lg.Target,
                             //                  Title = lg.Title,
                             //                  URL = lg.URL,

                             //              }).ToList(),
                         }).ToList();
                #endregion

                #region hotel order by
                var allhotels = new List<ParvazPardaz.Model.Entity.Hotel.Hotel>();
                switch (orby)
                {
                    case "mostrate":
                        allhotels = _dbSetHotel.OrderByDescending(x => x.PostRateAvg).ToList();
                        break;

                    case "mosthotelrank":
                        allhotels = _dbSetHotel.OrderBy(x => x.HotelRank.OrderId).ToList();
                        break;

                    case "alphabet":
                        allhotels = _dbSetHotel.OrderBy(x => x.Title).ToList();
                        break;

                    case "mostreviewcount":
                        allhotels = _dbSetHotel.Join(_unitOfWork.Set<EntityComment>(), jH => jH.Id, jC => jC.OwnId, (jH, jC) => new { jH, jC })
                            .Where(w => w.jC.IsApproved && w.jC.IsApproved)
                            .GroupBy(g => new { g.jH })
                            .Select(s => new { hotel = s.Key.jH, commentCount = s.Count() })
                            .OrderByDescending(o => o.commentCount)
                            .Distinct()
                            .Select(h => h.hotel)
                            .ToList();
                        break;

                    default:
                        allhotels = _dbSetHotel.ToList();
                        break;
                }
                #endregion
                #region hotels
                var hotels = (from d in allhotels
                              join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                              on d.Id equals l.typeId
                              where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && d.Tags.Any(x => x.Id == id && x.IsDeleted == false) //&& d.PostGroups.Any(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                              //orderby d.CreatorDateTime descending
                              select new PostListDetailViewModel
                              {
                                  Id = d.Id,
                                  Image = d.HotelGalleries.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                                  {
                                      ImageUrl = x.ImageUrl,
                                      Height = x.Height,
                                      Id = x.Id,
                                      ImageDesc = x.ImageDesc,
                                      ImageExtension = x.ImageExtension,
                                      ImageFileName = x.ImageFileName,
                                      ImageTitle = x.ImageTitle,
                                      IsPrimarySlider = x.IsPrimarySlider,
                                      Width = x.Width
                                  }).FirstOrDefault(),
                                  Thumbnail = d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? d.HotelGalleries.FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.FirstOrDefault().ImageExtension : d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension,
                                  Name = d.Title,
                                  PostRateAvg = d.PostRateAvg,
                                  PostRateCount = d.PostRateCount,
                                  PostSummery = d.Summary,
                                  PublishDatetime = d.PublishDatetime,
                                  VisitCount = l.VisitCount,
                                  Rel = l.Rel,
                                  Target = l.Target,
                                  URL = l.URL,
                                  Title = l.Title,
                                  //PostGroups = (from pg in d.PostGroups
                                  //              join lg in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                  //                  on pg.Id equals lg.typeId
                                  //              where lg.linkType == LinkType.PostGroup
                                  //              select new PostGroupViewModel
                                  //              {
                                  //                  Name = lg.Name,
                                  //                  Rel = lg.Rel,
                                  //                  Target = lg.Target,
                                  //                  Title = lg.Title,
                                  //                  URL = lg.URL
                                  //              }).ToList(),
                              }).ToList();

                if (hotels != null && hotels.Any())
                {
                    hasAnyHotel = true;
                }
                items.AddRange(hotels);
                #endregion
            }

            hasHotel = hasAnyHotel;

            return items;
        }
        #endregion

        #region searching
        public IList<PostListDetailViewModel> PostSearchLists(out bool isBeHiddenBtnMore, int pageIndex, int pageSize, string q)
        {
            List<PostListDetailViewModel> items = new List<PostListDetailViewModel>();

            #region post with tag & postgroup
            items = (from d in _dbSet
                     join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                     on d.Id equals l.typeId
                     where l.linkType == LinkType.Post && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && (d.Name.Contains(q) || l.Description.Contains(q) || l.Keywords.Contains(q))
                     orderby l.VisitCount descending
                     select new PostListDetailViewModel
                     {
                         Id = d.Id,
                         Image = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).Select(x => new ImageViewModel
                         {
                             ImageUrl = x.ImageUrl,
                             Height = x.Height,
                             Id = x.Id,
                             ImageDesc = x.ImageDesc,
                             ImageExtension = x.ImageExtension,
                             ImageFileName = x.ImageFileName,
                             ImageTitle = x.ImageTitle,
                             IsPrimarySlider = x.IsPrimarySlider,
                             Width = x.Width
                         }).FirstOrDefault(),
                         Thumbnail = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageUrl + "-765x535" + d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageExtension,
                         Name = d.Name,
                         PostRateAvg = d.PostRateAvg,
                         PostRateCount = d.PostRateCount,
                         PostSummery = d.PostSummery,
                         PublishDatetime = d.PublishDatetime,
                         VisitCount = l.VisitCount,
                         Rel = l.Rel,
                         Target = l.Target,
                         URL = l.URL,
                         Title = l.Title,
                     }).ToList();
            #endregion

            #region hotel with tag & postgroup
            var hotels = (from d in _dbSetHotel
                          join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                          on d.Id equals l.typeId
                          where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && (d.Title.Contains(q))
                          orderby l.VisitCount descending
                          select new PostListDetailViewModel
                          {
                              Id = d.Id,

                              Thumbnail = d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? d.HotelGalleries.FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.FirstOrDefault().ImageExtension : d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension,
                              Name = d.Title,
                              PostRateAvg = d.PostRateAvg,
                              PostRateCount = d.PostRateCount,
                              PostSummery = d.Summary,
                              PublishDatetime = d.PublishDatetime,
                              VisitCount = l.VisitCount,
                              Rel = l.Rel,
                              Target = l.Target,
                              URL = l.URL,
                              Title = l.Title,
                          }).ToList();

            items.AddRange(hotels);
            #endregion

            #region tour with tag & postgroup
            var tours = (from d in _unitOfWork.Set<ParvazPardaz.Model.Entity.Tour.Tour>()
                         join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                         on d.Id equals l.typeId
                         where l.linkType == LinkType.Tour && l.Visible && !l.IsDeleted && !d.IsDeleted && d.Recomended && (d.Title.Contains(q))
                         orderby l.VisitCount descending
                         select new PostListDetailViewModel
                         {
                             Id = d.Id,
                             Thumbnail = d.TourSliders.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageUrl,
                             Name = d.Title,
                             PostRateAvg = 0,
                             PostRateCount = 0,
                             PostSummery = d.ShortDescription,
                             PublishDatetime = d.CreatorDateTime.Value,
                             VisitCount = l.VisitCount,
                             Rel = l.Rel,
                             Target = l.Target,
                             URL = l.URL,
                             Title = l.Title

                         }).ToList();

            items.AddRange(tours);
            #endregion

            var totalCount = items.Count;
            items = items.OrderByDescending(x => x.VisitCount).Skip(pageIndex * pageSize).Take(pageSize).ToList();

            isBeHiddenBtnMore = !(((totalCount / pageSize) - (totalCount % pageSize == 0 ? 1 : 0)) > pageIndex);
            return items;
        }

        public List<TabMagPost> MagSearchLists(out bool isBeHiddenBtnMore, int pageIndex, int pageSize, string q)
        {

            List<TabMagPost> items = new List<TabMagPost>();

            #region post with tag & postgroup
            items = (from d in _dbSet
                     join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                     on d.Id equals l.typeId
                     where l.linkType == LinkType.Post && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && (d.Name.Contains(q) || l.Description.Contains(q) || l.Keywords.Contains(q))
                     orderby l.VisitCount descending
                     select new TabMagPost
                     {
                         Name = d.Name,
                         PostUrl = l.URL,
                         ImageUrl = d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageUrl + "-765x535" + d.PostImages.Where(x => x.Width == 765 || x.Width == 700).FirstOrDefault().ImageExtension,
                         PostSummery = d.PostSummery,
                         Rel = l.Rel,
                         Target = l.Target

                     }).ToList();
            #endregion

            #region hotel with tag & postgroup
            var hotels = (from d in _dbSetHotel
                          join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                          on d.Id equals l.typeId
                          where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && (d.Title.Contains(q))
                          orderby l.VisitCount descending
                          select new TabMagPost
                          {
                              Name = d.Title,
                              PostUrl = l.URL,
                              ImageUrl = d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? d.HotelGalleries.FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.FirstOrDefault().ImageExtension : d.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-261x177" + d.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension,
                              PostSummery = d.Summary,
                              Rel = l.Rel,
                              Target = l.Target

                          }).ToList();

            items.AddRange(hotels);
            #endregion

            var totalCount = items.Count;
            items = items.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            isBeHiddenBtnMore = !(((totalCount / pageSize) - (totalCount % pageSize == 0 ? 1 : 0)) > pageIndex);
            return items;
        }
        #endregion

        #region like
        public int like(int? id, string type, HttpRequestBase req)
        {

            var post = _dbSet.Find(id);
            if (post.LikeCount == null)
            {
                post.LikeCount = 1;
            }
            else
            {
                if (req.Cookies["like"] == null)
                {
                    post.LikeCount = post.LikeCount + 1;
                    List<DataCookie> productIdList = new List<DataCookie>();
                    DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now };
                    productIdList.Add(newData);

                    HttpCookie newRateProductIdListCookie = new HttpCookie("RateProductIdList")
                    {
                        Value = JsonConvert.SerializeObject(productIdList),
                        Expires = DateTime.Now.AddDays(30d)
                    };
                    //Response.SetCookie(newRateProductIdListCookie);
                }


            }

            _unitOfWork.SaveAllChanges();
            return post.LikeCount.Value;
        }
        #endregion

        #region CheckURL
        public bool CheckURL(string Name)
        {
            //var urlforbid= Name.Contains(',');
            var url = "/tourism/" + Name.Replace(" ", "-") + "/";
            var link = _unitOfWork.Set<LinkTable>().Where(x => x.URL == url && x.IsDeleted == false).FirstOrDefault();
            if (link == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
