using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public enum ContentStatus
    {
        Submitted = 0,//新建未审核
        Approved = 1,//审核通过
        Denied = 2,//审核失败
        Draft = 3,//草稿
        Deleted = 4,//删除
        None = -1//未知
    }
}
