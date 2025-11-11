using System;
using System.Windows.Forms;

namespace hwh.Models
{
    /// <summary>
    /// 메뉴 항목 정보를 담는 모델 클래스
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 메뉴 고유 ID
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 메뉴 표시 이름
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 메뉴 아이콘 (이모지 또는 문자)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 탭 ID (동일한 메뉴는 같은 탭 ID를 가짐)
        /// </summary>
        public string TabId { get; set; }

        /// <summary>
        /// UserControl 생성 팩토리 함수
        /// </summary>
        public Func<UserControl> ControlFactory { get; set; }

        /// <summary>
        /// 메뉴 순서
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 하단에 고정할 메뉴인지 여부
        /// </summary>
        public bool IsBottomFixed { get; set; }

        public MenuItem(string menuId, string menuName, string tabId, Func<UserControl> controlFactory, int order = 0, string icon = "●", bool isBottomFixed = false)
        {
            MenuId = menuId;
            MenuName = menuName;
            TabId = tabId;
            ControlFactory = controlFactory;
            Order = order;
            Icon = icon;
            IsBottomFixed = isBottomFixed;
        }
    }
}

