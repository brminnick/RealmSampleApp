using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using RealmSampleApp;
using RealmSampleApp.iOS;

[assembly: ExportRenderer(typeof(ContactsListViewCell), typeof(ContactsListViewCellCustomRenderer))]
namespace RealmSampleApp.iOS
{
	public class ContactsListViewCellCustomRenderer : TextCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			return cell;
		}
	}
}
