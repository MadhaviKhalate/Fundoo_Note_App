using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity Create(LabelModel labelModel, long userId);
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID);
        public LabelEntity DeleteLabel(long labelID, long userId);
        public IEnumerable<LabelEntity> GetLabels(long userId);
    }
}
