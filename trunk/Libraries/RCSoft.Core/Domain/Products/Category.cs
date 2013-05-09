using System;

namespace RCSoft.Core.Domain.Products
{
    public partial class Category : BaseEntity
    {
        /// <summary>
        /// 商品类别名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 父类别编号
        /// </summary>
        public virtual int ParentCategoryId { get; set; }

        /// <summary>
        /// 商品分类描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 分类图片路径
        /// </summary>
        public virtual string PictureUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateDate { get; set; }
    }
}
