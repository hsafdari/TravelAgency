using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using RefactorThis.GraphDiff;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Enum;
using EntityNS = ParvazPardaz.Model.Entity.Tour;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourService : BaseService<Model.Entity.Tour.Tour>, ITourService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Model.Entity.Tour.Tour> _dbSet;
        private readonly IMappingEngine _mappingEngine;

        private readonly ITourCategoryService _tourCategoryService;
        private readonly ITourLevelService _tourLevelService;
        private readonly ITourTypeService _tourTypeService;
        private readonly IAllowedBannedService _allowedBannedService;
        #endregion

        #region Ctor
        public TourService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, ITourCategoryService tourCategoryService, ITourLevelService tourLevelService
                            , ITourTypeService tourTypeService, IAllowedBannedService allowedBannedService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Model.Entity.Tour.Tour>();
            _tourCategoryService = tourCategoryService;
            _tourLevelService = tourLevelService;
            _tourTypeService = tourTypeService;
            _allowedBannedService = allowedBannedService;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourViewModel>(_mappingEngine);
        }
        #endregion

        #region CreateTour
        public ParvazPardaz.Model.Entity.Tour.Tour CreateTour(AddTourViewModel addTourViewModel)
        {
            var model = _mappingEngine.Map<ParvazPardaz.Model.Entity.Tour.Tour>(addTourViewModel);

            #region RequiredDocuments
            if (addTourViewModel.RequiredDocumentIds != null && addTourViewModel.RequiredDocumentIds.Any())
            {
                model.RequiredDocuments = new HashSet<RequiredDocument>();
                foreach (var RequiredDocId in addTourViewModel.RequiredDocumentIds.ToList())
                {
                    var requiredDoc = _unitOfWork.Set<RequiredDocument>().Find(RequiredDocId);
                    if (requiredDoc != null)
                    {
                        model.RequiredDocuments.Add(requiredDoc);
                    }
                }
            }
            #endregion

            #region Tour's Groups
            //افزودن گروه های تور _ همان پست-گروپ ها
            var postGroups = _unitOfWork.Set<PostGroup>();
            if (addTourViewModel._selectedPostGroups != null && addTourViewModel._selectedPostGroups.Any())
            {
                model.PostGroups = new HashSet<PostGroup>();
                foreach (var sPostGroupId in addTourViewModel._selectedPostGroups.ToList())
                {
                    var pg = postGroups.FirstOrDefault(x => x.Id == sPostGroupId);
                    if (pg != null)
                    {
                        model.PostGroups.Add(pg);
                    }
                }
            }
            #endregion

            #region TourCategories
            if (addTourViewModel.SelectedTourCategory != null && addTourViewModel.SelectedTourCategory.Any())
            {
                var tourCategories = _tourCategoryService.Filter(t => addTourViewModel.SelectedTourCategory.Any(i => i == t.Id)).ToList();
                model.TourCategories = new List<TourCategory>();
                foreach (var item in tourCategories)
                {
                    model.TourCategories.Add(item);
                }
            }
            #endregion

            #region TourLevels
            if (addTourViewModel.SelectedTourLevel != null && addTourViewModel.SelectedTourLevel.Any())
            {
                var tourLevels = _tourLevelService.Filter(t => addTourViewModel.SelectedTourLevel.Any(i => i == t.Id)).ToList();
                model.TourLevels = new List<TourLevel>();
                foreach (var item in tourLevels)
                {
                    model.TourLevels.Add(item);
                }
            }
            #endregion

            #region TourTypes
            if (addTourViewModel.SelectedTourType != null && addTourViewModel.SelectedTourType.Any())
            {
                var tourTypes = _tourTypeService.Filter(t => addTourViewModel.SelectedTourType.Any(i => i == t.Id)).ToList();
                model.TourTypes = new List<TourType>();
                foreach (var item in tourTypes)
                {
                    model.TourTypes.Add(item);
                }
            }
            #endregion

            #region SelectedAllows
            model.TourAllowBans = new HashSet<TourAllowBanned>();

            if (addTourViewModel.SelectedAllows != null && addTourViewModel.SelectedAllows.Any())
            {
                var allows = _allowedBannedService.Filter(t => addTourViewModel.SelectedAllows.Any(i => i == t.Id)).ToList();
                model.TourAllowBans = new List<TourAllowBanned>();
                foreach (var allow in allows)
                {
                    model.TourAllowBans.Add(new TourAllowBanned() { AllowedBannedId = allow.Id, TourId = model.Id, IsAllowed = true });
                }
            }
            #endregion

            #region SelectedBans
            if (addTourViewModel.SelectedBans != null && addTourViewModel.SelectedBans.Any())
            {
                var bans = _allowedBannedService.Filter(t => addTourViewModel.SelectedBans.Any(i => i == t.Id)).ToList();
                foreach (var ban in bans)
                {
                    model.TourAllowBans.Add(new TourAllowBanned() { AllowedBannedId = ban.Id, TourId = model.Id, IsAllowed = false });
                }
            }
            #endregion

            #region UnAvalilable Selected TourLandingPageUrl
            var landingPageUrl = _unitOfWork.Set<TourLandingPageUrl>().FirstOrDefault(x => x.Id == model.TourLandingPageUrlId);
            if (landingPageUrl != null)
            {
                landingPageUrl.IsAvailable = false;
            }
            #endregion

            _dbSet.Add(model);
            _unitOfWork.SaveAllChanges();

            return model;
        }
        #endregion

        #region UpdateTour
        public ParvazPardaz.Model.Entity.Tour.Tour UpdateTour(EditTourViewModel viewModel)
        {
            var model = base.GetById(t => t.Id == viewModel.Id);

            #region Update previous and new tour's landing page url
            //فعال کردن آدرس قبلی که برای این تور انتخاب شده بوده(در صورت وجود) ، برای اینکه برای تور دیگر قابل استفاده باشد
            var prevoiusTourLandPUrl = _unitOfWork.Set<TourLandingPageUrl>().FirstOrDefault(x => x.Id == model.TourLandingPageUrlId.Value);
            if (prevoiusTourLandPUrl != null)
            {
                prevoiusTourLandPUrl.IsAvailable = true;
            }

            //اگه تور فعال باشه ، به طور قطع باید براش آدرسی انتخاب شده باشه
            //if (viewModel.Recomended && viewModel.TourLandingPageUrlId != null && viewModel.TourLandingPageUrlId > 0)
            //{
            //    //غیر فعال کردن آدرس تور انتخاب شده برای این تور
            //    var selectedTourLandPUrl = _unitOfWork.Set<TourLandingPageUrl>().FirstOrDefault(x => x.Id == viewModel.TourLandingPageUrlId.Value);
            //    if (selectedTourLandPUrl != null)
            //    {
            //        selectedTourLandPUrl.IsAvailable = false;
            //    }
            //}
            #endregion

            _mappingEngine.Map(viewModel, model);

            #region Update Required Documents
            //حذف قبلی ها
            if (model.RequiredDocuments != null && model.RequiredDocuments.Any())
            {
                var requiredDocumentList = model.RequiredDocuments.ToList();
                foreach (var item in requiredDocumentList)
                {
                    model.RequiredDocuments.Remove(item);
                }
            }
            //مدارک مورد نیاز جدید
            if (viewModel.RequiredDocumentIds != null && viewModel.RequiredDocumentIds.Any())
            {
                model.RequiredDocuments = new HashSet<RequiredDocument>();
                foreach (var RequiredDocId in viewModel.RequiredDocumentIds.ToList())
                {
                    var requiredDoc = _unitOfWork.Set<RequiredDocument>().Find(RequiredDocId);
                    if (requiredDoc != null)
                    {
                        model.RequiredDocuments.Add(requiredDoc);
                    }
                }
            }
            #endregion

            #region update AllowBans
            #region Remove Previous AllowBans
            // آپدیت جدول میانی در صورتی که جدول میانی در مدل برنامه وجود داشته باشد
            foreach (var item in model.TourAllowBans.ToList())
            {
                _unitOfWork.MarkAsDeleted<TourAllowBanned>(item);
            }
            #endregion

            #region SelectedAllows
            model.TourAllowBans = new List<TourAllowBanned>();
            if (viewModel.SelectedAllows != null && viewModel.SelectedAllows.Any())
            {
                // model.TourAllowBans = new List<TourAllowBanned>();
                var allows = _allowedBannedService.Filter(a => viewModel.SelectedAllows.Any(x => x == a.Id));
                foreach (var allow in allows)
                {
                    model.TourAllowBans.Add(new TourAllowBanned()
                    {
                        IsAllowed = true,
                        AllowedBannedId = allow.Id,
                        TourId = model.Id
                    });
                }
            }
            #endregion

            #region SelectedBans
            if (viewModel.SelectedBans != null && viewModel.SelectedBans.Any())
            {
                //model.TourAllowBans = new List<TourAllowBanned>();
                var bans = _allowedBannedService.Filter(a => viewModel.SelectedBans.Any(x => x == a.Id));
                foreach (var ban in bans)
                {
                    model.TourAllowBans.Add(new TourAllowBanned()
                    {
                        AllowedBannedId = ban.Id,
                        TourId = model.Id,
                        IsAllowed = false
                    });
                }
            }
            #endregion
            #endregion

            #region Update Tour's groups
            //حذف  گروه های تور قبلی 
            foreach (var item in model.PostGroups.ToList())
            {
                model.PostGroups.Remove(item);
            }

            //افزودن گروه های تور _ همان پست-گروپ ها
            var postGroups = _unitOfWork.Set<PostGroup>();
            if (viewModel._selectedPostGroups != null && viewModel._selectedPostGroups.Any())
            {
                model.PostGroups = new HashSet<PostGroup>();
                foreach (var sPostGroupId in viewModel._selectedPostGroups.ToList())
                {
                    var pg = postGroups.FirstOrDefault(x => x.Id == sPostGroupId);
                    if (pg != null)
                    {
                        model.PostGroups.Add(pg);
                    }
                }
            }
            #endregion

            #region TourCategories
            // آپدیت کالکش - many to many
            foreach (var item in model.TourCategories.ToList())
            {
                model.TourCategories.Remove(item);
            }
            if (viewModel.SelectedTourCategory != null && viewModel.SelectedTourCategory.Any())
            {
                var tourCategories = _tourCategoryService.Filter(a => viewModel.SelectedTourCategory.Any(x => x == a.Id)).ToList();
                model.TourCategories = tourCategories;
            }
            #endregion

            #region TourLevels
            foreach (var item in model.TourLevels.ToList())
            {
                model.TourLevels.Remove(item);
            }
            if (viewModel.SelectedTourLevel != null && viewModel.SelectedTourLevel.Any())
            {
                var tourLevels = _tourLevelService.Filter(a => viewModel.SelectedTourLevel.Any(x => x == a.Id)).ToList();
                model.TourLevels = tourLevels;
            }
            #endregion

            #region TourTypes
            foreach (var item in model.TourTypes.ToList())
            {
                model.TourTypes.Remove(item);
            }
            if (viewModel.SelectedTourType != null && viewModel.SelectedTourType.Any())
            {
                var tourTypes = _tourTypeService.Filter(a => viewModel.SelectedTourType.Any(x => x == a.Id)).ToList();
                model.TourTypes = tourTypes;
            }
            #endregion

            _unitOfWork.SaveAllChanges(false);
            return model;
        }
        #endregion

        #region CheckURLIN
        public bool CheckURLInLinkTable(string LinkTableTitle)
        {
            //var urlforbid= Name.Contains(',');
            var url = "/tour/" + LinkTableTitle.Replace(" ", "-") + "/";
            var link = _unitOfWork.Set<LinkTable>().Where(x => x.URL == url && x.IsDeleted == false).FirstOrDefault();

            if (link == null) return true;
            else return false;
        }
        #endregion

        #region RelatedTours
        public IList<PostListDetailViewModel> RelatedTours(int cityId)
        {
            List<PostListDetailViewModel> tours = new List<PostListDetailViewModel>();

            if (cityId > 0)
            {
                #region Tours
               
                tours = (from t in _dbSet
                         join tlp in _unitOfWork.Set<TourLandingPageUrl>().AsEnumerable() on t.TourLandingPageUrlId equals tlp.Id
                         join l in _unitOfWork.Set<LinkTable>().AsEnumerable() on tlp.Id equals l.typeId
                         where l.linkType == LinkType.TourLanding && tlp.CityId == cityId
                         orderby t.CreatorDateTime descending
                         select new PostListDetailViewModel
                         {
                             Id = t.Id,
                             PostRateAvg = l.PostRateAvg.Value > 0 ? l.PostRateAvg.Value : 0,
                             PostRateCount = l.PostRateCount.Value > 0 ? l.PostRateCount.Value : 0,
                             PostSummery = t.ShortDescription,
                             PublishDatetime = t.CreatorDateTime.Value,
                             VisitCount = l.VisitCount,
                             Image = t.TourSliders.Where(x => x.IsPrimarySlider).Select(x => new ImageViewModel
                             {
                                 ImageUrl = x.ImageUrl,
                                 Height = 177,
                                 Id = x.Id,
                                 ImageDesc = t.Title,
                                 ImageExtension = x.ImageExtension,
                                 ImageFileName = x.ImageFileName,
                                 ImageTitle = t.Title,
                                 IsPrimarySlider = x.IsPrimarySlider,
                                 Width = 261

                             }).FirstOrDefault(),
                             Name = l.Name,
                             Rel = l.Rel,
                             Target = l.Target,
                             Title = l.Title,
                             URL = l.URL,

                         }).OrderBy(x => Guid.NewGuid()).ToList();
                #endregion
            }

            return tours;
        }
        #endregion

        #region SearchTours
        public List<TabMagPost> SearchTours(string q)
        {
            List<TabMagPost> findedTours = _dbSet.Join(_unitOfWork.Set<TourLandingPageUrl>().Where(x => !x.IsDeleted && !x.IsAvailable), jt => jt.TourLandingPageUrlId, jtlp => jtlp.Id, (jt, jtlp) => new { jt, jtlp })
                .Join(_unitOfWork.Set<LinkTable>().Where(l => !l.IsDeleted && l.Visible && l.linkType == LinkType.TourLanding), j1 => j1.jtlp.Id, jlt => jlt.typeId, (j1, jlt) => new { j1, jlt })
                .Where(w => (w.j1.jt.Title.Contains(q) || w.j1.jt.ShortDescription.Contains(q) || w.j1.jt.Description.Equals(q) || w.j1.jt.PostGroups.Any(tag => tag.Name.Equals(q) || tag.Title.Equals(q))) && !w.j1.jt.IsDeleted && w.j1.jt.Recomended)
                .Select(s => new TabMagPost()
                {
                    //ImageUrl = (from post in _unitOfWork.Set<Post>() where post.Id == x.jrp.Id select post.PostImages.FirstOrDefault(i => i.IsPrimarySlider).ImageUrl).FirstOrDefault(),
                    ImageUrl = s.j1.jt.TourSliders.FirstOrDefault(x => x.IsPrimarySlider).ImageUrl ?? "",
                    Name = s.jlt.Title,
                    PostSummery = s.j1.jt.ShortDescription,
                    PostUrl = s.j1.jtlp.URL,
                    Target = s.jlt.Target,
                    Rel = s.jlt.Rel

                }).ToList();

            return findedTours;
        }

        #endregion

        #region GetTourDDL
        public SelectList GetTourDDL()
        {
            return new SelectList(_dbSet.Where(x => !x.IsDeleted).Select(x => new { Id = x.Id, Title = (!x.Recomended ? "(" + ParvazPardaz.Resource.General.Generals.DeActive + ") " : "") + x.Title }), "Id", "Title"); // && x.Recomended به درخواست آقای لباف
        }
        #endregion
    }
}
