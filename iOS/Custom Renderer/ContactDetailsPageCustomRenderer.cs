using System.Collections.Generic;
using RealmSampleApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(ContactDetailsPageCustomRenderer))]
namespace RealmSampleApp.iOS
{
    public class ContactDetailsPageCustomRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = (Page)Element;

            var leftNavList = new List<UIBarButtonItem>();
            var rightNavList = new List<UIBarButtonItem>();

            var navigationItem = NavigationController.TopViewController.NavigationItem;

            for (var i = 0; i < page.ToolbarItems.Count; i++)
            {
                var reorder = page.ToolbarItems.Count - 1;
                var itemPriority = page.ToolbarItems[reorder - i].Priority;

                if (itemPriority is 1)
                {
                    var leftNavItems = navigationItem?.RightBarButtonItems?[i];

                    if (leftNavItems is not null)
                        leftNavList.Add(leftNavItems);
                }
                else if (itemPriority is 0)
                {
                    var rightNavigationItems = navigationItem?.RightBarButtonItems?[i];

                    if (rightNavigationItems is not null)
                        rightNavList.Add(rightNavigationItems);
                }
            }

            navigationItem?.SetLeftBarButtonItems(leftNavList.ToArray(), false);
            navigationItem?.SetRightBarButtonItems(rightNavList.ToArray(), false);
        }
    }
}