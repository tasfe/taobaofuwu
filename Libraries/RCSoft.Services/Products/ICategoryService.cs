using RCSoft.Core.Domain.Products;
using System.Collections.Generic;
using RCSoft.Core;

namespace RCSoft.Services.Products
{
    public interface ICategoryService
    {
        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="category">类别实体</param>
        void DeleteCategory(Category category);

        /// <summary>
        /// 获取所有产品类别
        /// </summary>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>所有类别列表</returns>
        IList<Category> GetAllCategories(bool showDeleted = false);

        /// <summary>
        /// 获取所有产品类别
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>所有类别列表</returns>
        IList<Category> GetAllCategories(string categoryName, bool showDeleted = false);

        /// <summary>
        /// 获取所有产品类别
        /// </summary>
        /// <param name="categoryName">类别名称</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>Categories</returns>
        IPagedList<Category> GetAllCategories(string categoryName, int pageIndex, int pageSize, bool showDeleted = false);

        /// <summary>
        /// 根据父类别编号获取类别
        /// </summary>
        /// <param name="parentCategoryId">父类别编号</param>
        /// <param name="showDeleted">是否显示被删除的记录</param>
        /// <returns>产品类别集合</returns>
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showDeleted = false);

        /// <summary>
        /// 根据编号获取一个类别
        /// </summary>
        /// <param name="categoryId">类别编号</param>
        /// <returns>类别</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// 插入一个类别
        /// </summary>
        /// <param name="category">类别</param>
        void InsertCategory(Category category);

        /// <summary>
        /// 更新类别
        /// </summary>
        /// <param name="category">类别</param>
        void UpdateCategory(Category category);

    }
}
