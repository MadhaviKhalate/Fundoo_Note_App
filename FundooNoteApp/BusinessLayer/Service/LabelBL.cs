using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL iLabelRL;

        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }
        public LabelEntity Create(LabelModel labelModel, long userId)
        {
            try
            {
                return iLabelRL.Create(labelModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                return iLabelRL.UpdateLabel(labelModel, labelID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity DeleteLabel(long labelID, long userId)
        {
            try
            {
                return iLabelRL.DeleteLabel(labelID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                return iLabelRL.GetLabels(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
