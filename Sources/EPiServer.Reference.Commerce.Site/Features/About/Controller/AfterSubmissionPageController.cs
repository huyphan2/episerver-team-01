using EPiServer;
using EPiServer.Core;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.Models;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Reference.Commerce.Site.Features.About.Blocks;
using EPiServer.Reference.Commerce.Site.Features.About.Pages;
using EPiServer.Reference.Commerce.Site.Features.About.ViewModels;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.About.Controller
{
    public class AfterSubmissionPageController : PageController<AfterSubmissionPage>, IAppendExtraInfoToRedirection
    {
        private IFormRepository _formRepository;
        private IFormDataRepository _formDataRepository;
        private IContentRepository _contentRepository;
        public AfterSubmissionPageController(
            IFormRepository formRepository,
            IFormDataRepository formDataRepository,
            IContentRepository contentRepository
            )
        {
            _formRepository = formRepository;
            _formDataRepository = formDataRepository;
            _contentRepository = contentRepository;
        }

        public IDictionary<string, object> GetExtraInfo(FormIdentity formIden, Submission submission)
        {
            throw new NotImplementedException();
        }

        public ActionResult Index(AfterSubmissionPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            AfterSubmissionViewModel model = new AfterSubmissionViewModel();
            var formGuid = Request.QueryString["__FormGuid"];
            var languageBranch = Request.QueryString["__FormLanguage"];
            var formSubmissionId = Request.QueryString["__FormSubmissionId"];
            if (!string.IsNullOrEmpty(formGuid))
            { 
                var submittedData = _formDataRepository.GetSubmissionData(
                new FormIdentity(new Guid(formGuid), languageBranch),
                DateTime.Now.AddDays(-100),
                DateTime.Now).FirstOrDefault(t => t.Id == formSubmissionId);

                //var currentForm = _contentRepository.Get<ContactUsBlock>(new Guid(formGuid));

                //if (currentForm != null)
                //{
                //    model.ContactUsBlock = currentForm;
                //    model.CurrentPage = currentPage;
                //}
            }


            return View(model);
        }
    }
}