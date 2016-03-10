using Marvin.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RestAPI
{
    [APIAuthorize()]
    public class LeadsController : ApiController
    {
        private static List<Lead> leadlist = new List<Lead>();
        public static List<System.Security.Claims.ClaimsPrincipal> claimlist = new List<System.Security.Claims.ClaimsPrincipal>();

        public LeadsController()
        {
        }

        [Scope("restapi", "read")]
        [LeadsRoute("api/Leads", 1)]
        [LeadsRoute("api/Leads/{id}", 1)]
        [HttpGet]
        public IHttpActionResult GetLead(int? id = null)
        {
            try
            {
                if (!id.HasValue) { return BadRequest(); }
                Lead l = LeadsController.leadlist.SingleOrDefault(lo => lo.Id == id.Value);
                if (l == null)
                {
                    return NotFound();
                }
                else
                {
                    l.Version = "1";
                    return Ok<Lead>(l);
                }
            }
            catch
            {
                // do error logging
                return InternalServerError();
            }
        }

        [Scope("restapi", "read")]
        [LeadsRoute("api/Leads", 2)]
        [LeadsRoute("api/Leads/{id}", 2)]
        [HttpGet]
        public IHttpActionResult GetLead2(int? id = null)
        {
            try
            {
                if (!id.HasValue) { return BadRequest(); }
                Lead l = LeadsController.leadlist.SingleOrDefault(lo => lo.Id == id.Value);
                if (l == null)
                {
                    return NotFound();
                }
                else
                {
                    l.Version = "2";
                    return Ok<Lead>(l);
                }
            }
            catch
            {
                // do error logging
                return InternalServerError();
            }
        }

        [Scope("restapi")]
        [LeadsRoute("api/Leads", 1)]
        [HttpPost]
        public IHttpActionResult CreateLead([FromBody] Lead entity)
        {
            try
            {
                if (!(entity == null))
                {
                    entity.Id = LeadsController.leadlist.Count == 0 ? 1 : LeadsController.leadlist.Max(lo => lo.Id) + 1;
                    LeadsController.leadlist.Add(entity);
                    return Ok<Lead>(entity);
                }

                return BadRequest();
            }
            catch
            {
                // do error logging
                return InternalServerError();
            }
        }

        [Scope("restapi")]
        [LeadsRoute("api/Leads", 1)]
        [HttpPut]
        public IHttpActionResult UpdateLead([FromBody] Lead entity)
        {
            try
            {
                if (!(entity == null))
                {
                    Lead l = LeadsController.leadlist.SingleOrDefault(lo => lo.Id == entity.Id);
                    if (!(l == null))
                    {
                        LeadsController.leadlist.Remove(l);
                        l.Name = entity.Name;
                        l.Address = entity.Address;
                        LeadsController.leadlist.Add(l);
                        return Ok<Lead>(entity);
                    }

                    return NotFound();
                }

                return BadRequest();
            }
            catch
            {
                // do error logging
                return InternalServerError();
            }
        }

        [Scope("restapi")]
        [LeadsRoute("api/Leads/{id}", 1)]
        [HttpPatch]
        public IHttpActionResult UpdateLead(int id, [FromBody] JsonPatchDocument<Lead> entity)
        {
            try
            {
                if (!(entity == null))
                {
                    Lead l = LeadsController.leadlist.SingleOrDefault(lo => lo.Id == id);
                    if (!(l == null))
                    {
                        LeadsController.leadlist.Remove(l);
                        entity.ApplyTo(l);
                        LeadsController.leadlist.Add(l);
                        return Ok<Lead>(l);
                    }

                    return NotFound();
                }

                return BadRequest();
            }
            catch
            {
                // do error logging
                return InternalServerError();
            }
        }

        [Scope("restapi")]
        [LeadsRoute("api/Leads/{id}", 1)]
        [HttpDelete]
        public IHttpActionResult DeleteLead(int id)
        {
            try
            {
                Lead l = LeadsController.leadlist.SingleOrDefault(lo => lo.Id == id);
                if (!(l == null))
                {
                    LeadsController.leadlist.Remove(l);
                    return new System.Web.Http.Results.StatusCodeResult(System.Net.HttpStatusCode.NoContent, this);
                }

                return NotFound();
            }
            catch
            {
                // do error logging
                return InternalServerError();
            }
        }
    }
}