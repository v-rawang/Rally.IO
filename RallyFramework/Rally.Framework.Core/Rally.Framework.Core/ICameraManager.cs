using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface ICameraManager
    {
        IList<InstrumentCameraSetting> LoadCameraSettingMeta(string Repository);

        T CreateCameraController<T>(InstrumentCameraSetting CameraSetting);
    }
}
