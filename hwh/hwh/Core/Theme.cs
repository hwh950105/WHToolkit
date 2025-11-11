using System.Drawing;

namespace hwh.Core
{
    /// <summary>
    /// 애플리케이션 전체 디자인 시스템
    /// </summary>
    public static class Theme
    {
        // ============================================
        // 브랜드 색상 (Gradient Purple-Blue Theme)
        // ============================================
        public static readonly Color Primary = Color.FromArgb(124, 58, 237);      // Purple-600
        public static readonly Color PrimaryHover = Color.FromArgb(109, 40, 217); // Purple-700
        public static readonly Color PrimaryDark = Color.FromArgb(109, 40, 217);  // Purple-700
        public static readonly Color PrimaryLight = Color.FromArgb(139, 92, 246); // Purple-500
        public static readonly Color Secondary = Color.FromArgb(59, 130, 246);    // Blue-500
        public static readonly Color Accent = Color.FromArgb(236, 72, 153);       // Pink-500

        // ============================================
        // 배경 색상 (Modern Layered Backgrounds)
        // ============================================
        public static readonly Color SidebarBg = Color.FromArgb(17, 24, 39);      // Gray-900
        public static readonly Color SidebarHover = Color.FromArgb(31, 41, 55);   // Gray-800
        public static readonly Color TabBarBg = Color.FromArgb(255, 255, 255);    // White
        public static readonly Color ContentBg = Color.FromArgb(249, 250, 251);   // Gray-50
        public static readonly Color ContentHover = Color.FromArgb(243, 244, 246); // Gray-100
        public static readonly Color CardBg = Color.FromArgb(255, 255, 255);      // White

        // ============================================
        // 텍스트 색상 (High Contrast)
        // ============================================
        public static readonly Color TextPrimary = Color.FromArgb(17, 24, 39);    // Gray-900
        public static readonly Color TextSecondary = Color.FromArgb(107, 114, 128); // Gray-500
        public static readonly Color TextLight = Color.FromArgb(229, 231, 235);   // Gray-200
        public static readonly Color TextWhite = Color.FromArgb(255, 255, 255);   // White

        // ============================================
        // 탭 색상
        // ============================================
        public static readonly Color TabInactive = Color.FromArgb(243, 244, 246);  // Gray-100
        public static readonly Color TabHover = Color.FromArgb(229, 231, 235);     // Gray-200
        public static readonly Color TabActive = Color.FromArgb(255, 255, 255);    // White

        // ============================================
        // 경계선 & 구분선
        // ============================================
        public static readonly Color Border = Color.FromArgb(229, 231, 235);       // Gray-200
        public static readonly Color BorderLight = Color.FromArgb(229, 231, 235);  // Gray-200
        public static readonly Color BorderDark = Color.FromArgb(55, 65, 81);     // Gray-700
        public static readonly Color Divider = Color.FromArgb(243, 244, 246);     // Gray-100

        // ============================================
        // 상태 색상
        // ============================================
        public static readonly Color Success = Color.FromArgb(34, 197, 94);       // Green-500
        public static readonly Color Warning = Color.FromArgb(251, 146, 60);      // Orange-400
        public static readonly Color Error = Color.FromArgb(239, 68, 68);         // Red-500
        public static readonly Color Info = Color.FromArgb(59, 130, 246);         // Blue-500

        // ============================================
        // 그림자 (Shadow Effects)
        // ============================================
        public static readonly Color ShadowLight = Color.FromArgb(30, 0, 0, 0);   // 12% opacity
        public static readonly Color ShadowMedium = Color.FromArgb(50, 0, 0, 0);  // 20% opacity
        public static readonly Color ShadowDark = Color.FromArgb(80, 0, 0, 0);    // 31% opacity

        // ============================================
        // 여백 & 간격 (Spacing System)
        // ============================================
        public const int SpacingXS = 4;
        public const int SpacingS = 8;
        public const int SpacingM = 12;
        public const int SpacingL = 16;
        public const int SpacingXL = 24;
        public const int SpacingXXL = 32;

        // ============================================
        // 폰트 (Typography)
        // ============================================
        public static readonly Font FontTitle = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font FontHeading = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font FontRegular = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBody = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBodyBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontSmall = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font FontMenu = new Font("Segoe UI", 10.5F, FontStyle.Regular);
        public static readonly Font FontMenuBold = new Font("Segoe UI", 10.5F, FontStyle.Bold);
    }
}
