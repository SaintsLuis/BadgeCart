using System;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using Google.Android.Material.Badge;

namespace BadgeCart
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            //return base.CreateBottomNavViewAppearanceTracker(shellItem);
            return new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        }
    }

    class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        private BadgeDrawable? _badgeDrawable;
        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }
        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            if (_badgeDrawable is null)
            {
                const int cartTabbarItemIndex = 1;

                _badgeDrawable = bottomView.GetOrCreateBadge(cartTabbarItemIndex);
                UpdateBadge(0);
                BadgeCounterService.CountChanged += OnCountChanged;
            }
        }

        private void OnCountChanged(object? sender, int newCount)
        {
            UpdateBadge(newCount);
        }

        private void UpdateBadge(int count)
        {
            if (_badgeDrawable is not null)
            {
                if (count <= 0)
                {
                    _badgeDrawable.SetVisible(false);
                }
                else
                {
                    _badgeDrawable.SetVisible(true);
                    _badgeDrawable.Number = count;
                    _badgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
                    _badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BadgeCounterService.CountChanged -= OnCountChanged;
        }
    }
}