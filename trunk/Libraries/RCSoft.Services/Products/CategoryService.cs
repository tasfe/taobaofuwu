using System;
using System.Linq;
using System.Collections.Generic;
using RCSoft.Core;
using RCSoft.Core.Domain.Products;
using RCSoft.Core.Data;

namespace RCSoft.Services.Products
{
    public partial class CategoryService:ICategoryService
    {
        #region 字段
        public readonly IRepository<Category> _categoryRepository;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="categoryRepository">分类</param>
        public CategoryService(IRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        #endregion

        #region 方法

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="category">类别实体</param>
        public virtual void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("商品类别");
            category.IsDeleted = true;
            UpdateCategory(category);
        }

        /// <summary>
        /// 获取所有产品类别
        /// </summary>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>所有类别列表</returns>
        public virtual IList<Category> GetAllCategories(bool showDeleted = false)
        {
            return GetAllCategories(null, showDeleted);
        }

        /// <summary>
        /// 获取所有产品类别
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>所有类别列表</returns>
        public virtual IList<Category> GetAllCategories(string categoryName, bool showDeleted = false)
        {
            var query = _categoryRepository.Table;
            if (!showDeleted)
                query = query.Where(c => c.IsDeleted);
            if (!string.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);

            return query.ToList();
        }

        /// <summary>
        /// 获取所有产品类别
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<Category> GetAllCategories(string categoryName, int pageIndex, int pageSize, bool showDeleted = false)
        {
            var categories = GetAllCategories(categoryName, true);
            return new PagedList<Category>(categories, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据父类别编号获取类别
        /// </summary>
        /// <param name="parentCategoryId">父类别编号</param>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>产品类别集合</returns>
        public IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showDeleted = false)
        {
            var query = _categoryRepository.Table;
            if (showDeleted)
                query = query.Where(c => c.IsDeleted);
            query = query.Where(c => c.ParentCategoryId == parentCategoryId);
            query = query.OrderBy(c => c.DisplayOrder);

            return query.ToList();
        }

        /// <summary>
        /// 根据编号获取一个类别
        /// </summary>
        /// <param name="categoryId">类别编号</param>
        /// <returns>类别</returns>
        public virtual Category GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;
            return _categoryRepository.GetById(categoryId);
        }

        /// <summary>
        /// 插入一个类别
        /// </summary>
        /// <param name="category">类别</param>
        public virtual void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("商品类别");
            _categoryRepository.Insert(category);
        }

        /// <summary>
        /// 更新类别
        /// </summary>
        /// <param name="category">类别</param>
        public virtual void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("商品类别");
            _categoryRepository.Update(category);
        }
        
        #endregion
    }
}
