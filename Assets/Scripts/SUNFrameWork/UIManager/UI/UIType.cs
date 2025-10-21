using SUNFrames;

namespace SUNFrames
{
    public class UIType
    {
        private string path;

        public string Path
        {
            get => path;
        }

        private string name;

        public string Name
        {
            get => name;
        }

        /// <summary>
        /// 获得UI信息
        /// </summary>
        /// <param name="ui_path">对应Panel的路径</param>
        /// <param name="ui_name">对应Panel的名称</param>
        public UIType(string ui_path, string ui_name)
        {
            name = ui_name;
            path = ui_path;
        }
    }
}
