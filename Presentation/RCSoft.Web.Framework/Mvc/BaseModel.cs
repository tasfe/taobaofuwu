using System.Web.Mvc;
namespace RCSoft.Web.Framework.Mvc
{
    public partial class BaseModel
    {
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        { }
    }
    public partial class BaseEntityModel : BaseModel
    {
        public virtual int Id { get; set; }
    }
}
