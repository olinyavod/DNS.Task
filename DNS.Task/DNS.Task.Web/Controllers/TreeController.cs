using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Owin;
using DNS.Task.Core.Models;
using DNS.Task.Core.Store;
using FormCollection = System.Web.Mvc.FormCollection;
using Microsoft.Owin.Extensions;

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
	        if (ModelState.IsValid)
	        {
		        node.Id = await Store.AddAsync(node, CancellationToken.None);
		        return Json(new {success = true, result = node});
	        }
	        return Json(new {success = false, reslt = "Invalid object!"}, JsonRequestBehavior.AllowGet);
        }

       

        // POST: Tree/Edit/5
        [HttpPost]
        public async Task<JsonResult> Edit(int id, Node node)
        {
	        if (ModelState.IsValid)
	        {
		        node.Id = id;
		        await Store.UpdateAsync(node, CancellationToken.None);
		        return Json(new {success = true, result = node}, JsonRequestBehavior.AllowGet);
	        }
	        return Json(new
	        {
		        success = false,
		        result = "Invalid object!"
	        }, JsonRequestBehavior.AllowGet);
        }

        // GET: Tree/Delete/5
        public async Task<JsonResult> Delete(int id)
        {
	        return Json(new {success = await Store.ForceDeleteAsync(id, CancellationToken.None)}, JsonRequestBehavior.AllowGet);
        }


	    public async Task<JsonResult> GetList(int? parenId)
	    {
		    var result = await Store.GetListAsync(new GetNodesRequest(), CancellationToken.None);
		    return Json(result.Select(i => new { id = i.Id, text = i.Title, type = i.NodeType.ToString().ToLower(), children = i.NodeType == NodeType.Folder }), JsonRequestBehavior.AllowGet);
	    }
    }
}
