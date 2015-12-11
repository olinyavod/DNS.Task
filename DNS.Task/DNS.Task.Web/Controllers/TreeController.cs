using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Owin;
using DNS.Task.Core.Models;
using DNS.Task.Core.Store;

namespace DNS.Task.Web.Controllers
{
    public class TreeController : Controller
    {
	    public ICrudStore<Node> Store
	    {
		    get { return Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<ICrudStore<Node>>(); }
	    }

	    // GET: Tree
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tree/Details/5
        public async Task<JsonResult> Details(int id)
        {
            return Json(await Store.GetAsync(id, CancellationToken.None), JsonRequestBehavior.AllowGet);
        }

        // POST: Tree/Create
        [HttpPost]
        public async Task<JsonResult> Create(Node node)
        {
	        try
	        {
		        if (ModelState.IsValid)
		        {
			        node.Id = await Store.AddAsync(node, CancellationToken.None);
			        return Json(new {success = true, result = node});
		        }
		        return Json(new {success = false, reslt = "Invalid object!"}, JsonRequestBehavior.AllowGet);
	        }
	        catch (Exception)
	        {
				Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<IUnitOfWork>().Rollback();
		        return Json(new {success = false});
	        }
        }

       

        // POST: Tree/Edit/5
        [HttpPost]
        public async Task<JsonResult> Edit(int id, Node node)
        {
	        try
	        {
		        if (ModelState.IsValid)
		        {
			        node.Id = id;
			        await Store.UpdateAsync(node, CancellationToken.None)
				        .ConfigureAwait(true);
			        return Json(new {success = true, result = node}, JsonRequestBehavior.AllowGet);
		        }
		        return Json(new
		        {
			        success = false,
			        result = "Invalid object!"
		        }, JsonRequestBehavior.AllowGet);
	        }
	        catch (Exception)
	        {
				Request.GetOwinContext().GetAutofacLifetimeScope().Resolve<IUnitOfWork>().Rollback();
		        return Json(new {success = false});
	        }
        }

        // GET: Tree/Delete/5
        public async Task<JsonResult> Delete(int id)
        {
	        return Json(new {success = await Store.ForceDeleteAsync(id, CancellationToken.None)}, JsonRequestBehavior.AllowGet);
        }


	    public async Task<JsonResult> GetList(int? parentId)
	    {
		    var result = await Store.GetListAsync(new GetNodesRequest(parentId), CancellationToken.None);
			return Json(result.Select(i => new
			{ 
				key = i.Id,
				title = i.Title, 
				isFolder = i.NodeType == NodeType.Folder, 
				hasChildren = i.NodeType == NodeType.Folder,
				isLazy = i.NodeType == NodeType.Folder,
				data = i
			}), JsonRequestBehavior.AllowGet);
	    }
    }
}
