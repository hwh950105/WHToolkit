using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using hwh.Models;
using hwh.Controls;
using hwh.Controls.Win32Controls;
using hwh.Controls.TrendChartControl;
using hwh.Data;

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
            // 센서 데이터 초기화 (테이블 생성 및 테스트 데이터 생성)
            try
            {
                 SensorDataSeeder.Initialize();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "센서 데이터 초기화 실패");
            }
            // 일반 메뉴
            RegisterMenu(new MenuItem(
                menuId: "menuId1",
                menuName: "사용자 관리",
                tabId: "tabId1",
                controlFactory: () => new DataListControl(),
                order: 1,
                icon: "👥"  // 사용자들 아이콘
            ));
            RegisterMenu(new MenuItem(
                menuId: "menuId2",
                menuName: "openAI",
                tabId: "tabId2",
                controlFactory: () => new apiControl(),
                order: 2,
                icon: "🔧" 
            ));

            RegisterMenu(new MenuItem(
                menuId: "menuId3",
                menuName: "Win32 API",
                tabId: "tabId3",
                controlFactory: () => new Win32TestControl(),
                order: 3,
                icon: "🪟"
            ));

            RegisterMenu(new MenuItem(
                menuId: "menuId5",
                menuName: "시계열 차트",
                tabId: "tabId5",
                controlFactory: () => CreateSensorChart(),
                order: 5,
                icon: "📈" // 추세 차트 아이콘
            ));



            // 하단 고정 메뉴
            RegisterMenu(new MenuItem(
                isBottomFixed: true, // 하단 고정
                menuId: "menuId_1",
                menuName: Globaldata.useremail,
                tabId: "tabId_1",
                controlFactory: () => new UserInfoControl(),
                order: 998,
                icon: "👤" // 사용자 아이콘

            ));


            RegisterMenu(new MenuItem(
                 isBottomFixed: true , // 하단 고정
                menuId: "menuId_2",
                menuName: "설정",
                tabId: "tabId_2",
                controlFactory: () => new SettingsControl(),
                order: 999,
                icon: "⚙️" // 설정 아이콘
               
            ));


        }

        /// <summary>
        /// 센서 데이터 차트 생성 (sensor_data 테이블 사용)
        /// </summary>
        private static ScottPlotTrendChart CreateSensorChart()
        {
            var chart = new ScottPlotTrendChart();

            try
            {
                // 센서 데이터 프로바이더 설정
                var dataProvider = new SensorDatabaseProvider();
                chart.DataProvider = dataProvider;

                // DB에서 사용 가능한 센서 타입 조회
                var sensorTypes = SensorDatabaseProvider.GetAvailableSensorTypes();

                if (sensorTypes.Count > 0)
                {
                    // 센서 타입별로 태그 추가
                    foreach (var sensorType in sensorTypes)
                    {
                        chart.AddTag(sensorType);
                    }

                    // 과거 1시간 데이터 로드
                    chart.LoadData(DateTime.Now.AddHours(-1), DateTime.Now);
                }
                else
                {
                    // 데이터가 없으면 빈 차트로 시작
                    chart.SetTitle("센서 데이터가 없습니다 (sensor_data 테이블 확인)");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "센서 차트 생성 오류");
                chart.SetTitle($"데이터 로드 오류: {ex.Message}");
            }

            return chart;
        }
    }
}

