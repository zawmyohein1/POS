using POS.Domain.EntityModels;
using POS.Infrastructure.Data.Context;
using POS.Infrastructure.Logger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;

namespace POS.Infrastructure.Data.Helper
{
    public static class AuditTrail
    {
        private static ILoggerHelper _logger = LoggerHelper.Instance;
        private static readonly DbContextOptions _option;

        public static void InsertAuditTrail(AuditAction eAction, AuditModule eModule, string Entity, string Username)
        {
            try
            {
                _logger.TraceLog(string.Format("eAction: {0},eModule: {1},Entity:{2},UserName:{3}", eAction, eModule, Entity, Username));

                using (var entity = new POSDbContext(_option))
                {
                    Audit_Trail oAuditTrail = new Audit_Trail();
                    oAuditTrail.Action_ID = (int)eAction;
                    oAuditTrail.Module_ID = (int)eModule;
                    oAuditTrail.Entity = Entity;
                    oAuditTrail.User_Name = Username;
                    oAuditTrail.Audit_Timestamp = DateTime.Now;

                    entity.Audit_Trails.Add(oAuditTrail);
                    entity.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                _logger.LogError(ex);
                foreach (var eve in ex.EntityValidationErrors)
                {
                    _logger.TraceLog(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        _logger.TraceLog(String.Format("- Property: \"{0}\",Error:\"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        public static string GetEntityInfo(Object Entity)
        {
            string Info = "";
            foreach (var prop in Entity.GetType().GetProperties())
            {
                if (!prop.PropertyType.IsAbstract && prop.PropertyType.Namespace == "System")
                {
                    Info += String.Format("{0}={1},", prop.Name, prop.GetValue(Entity, null));
                }
            }
            return Info;
        }
    }
}
