using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using hwh.Models;
using hwh.Controls;

namespace hwh.Core
{
    /// <summary>
    /// 메뉴 등록 및 관리를 담당하는 클래스
    /// </summary>
    public static class MenuRegistry
    {
        private static readonly List<MenuItem> _menuItems = new List<MenuItem>();

        /// <summary>
        /// 메뉴를 등록합니다.
        /// </summary>
        public static void RegisterMenu(MenuItem menuItem)
        {
            if (_menuItems.Any(m => m.MenuId == menuItem.MenuId))
            {
                throw new InvalidOperationException($"메뉴 ID '{menuItem.MenuId}'가 이미 등록되어 있습니다.");
            }
            _menuItems.Add(menuItem);
        }

        /// <summary>
        /// 등록된 모든 메뉴를 순서대로 반환합니다.
        /// </summary>
        public static IEnumerable<MenuItem> GetAllMenus()
        {
            return _menuItems.OrderBy(m => m.Order);
        }

        /// <summary>
        /// 일반 메뉴만 반환합니다 (하단 고정 제외)
        /// </summary>
        public static IEnumerable<MenuItem> GetNormalMenus()
        {
            return _menuItems.Where(m => !m.IsBottomFixed).OrderBy(m => m.Order);
        }

        /// <summary>
        /// 하단 고정 메뉴만 반환합니다
        /// </summary>
        public static IEnumerable<MenuItem> GetBottomMenus()
        {
            return _menuItems.Where(m => m.IsBottomFixed).OrderBy(m => m.Order);
        }

        /// <summary>
        /// 메뉴 ID로 메뉴를 찾습니다.
        /// </summary>
        public static MenuItem? GetMenuById(string menuId)
        {
            return _menuItems.FirstOrDefault(m => m.MenuId == menuId);
        }

        /// <summary>
        /// 모든 메뉴를 초기화합니다.
        /// </summary>
        public static void Clear()
        {
            _menuItems.Clear();
        }

        /// <summary>
        /// 기본 메뉴들을 등록합니다.
        /// </summary>
        public static void RegisterDefaultMenus()
        {
            // 일반 메뉴
            RegisterMenu(new MenuItem(
                menuId: "MENU_DATA_LIST",
                menuName: "사용자 관리",
                tabId: "TAB_DATA_LIST",
                controlFactory: () => new dbDataListControl(),
                order: 1,
                icon: "👥"  // 사용자들 아이콘
            ));

            // 하단 고정 메뉴
            RegisterMenu(new MenuItem(
                isBottomFixed: true, // 하단 고정
                menuId: "MENU_USER_INFO",
                menuName: Globaldata.useremail,
                tabId: "TAB_USER_INFO",
                controlFactory: () => new UserInfoControl(),
                order: 998,
                icon: "👤" // 사용자 아이콘
      
            ));

            RegisterMenu(new MenuItem(
                 isBottomFixed: true , // 하단 고정
                menuId: "MENU_SETTINGS",
                menuName: "설정",
                tabId: "TAB_SETTINGS",
                controlFactory: () => new SettingsControl(),
                order: 999,
                icon: "⚙️" // 설정 아이콘
               
            ));



            RegisterMenu(new MenuItem(

                menuId: "MENU2",
                menuName: "개발중",
                tabId: "TAB2",
                controlFactory: () => new apiControl(),
                order: 2,
                icon: "⚙️" // 설정 아이콘

            ));


            
        }
    }
}

