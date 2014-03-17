using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Automation.Peers;
using Infragistics.Controls.Schedules.Primitives;
using System.Windows.Automation.Provider;
using System.Collections.Generic;
using System.Diagnostics;
using Infragistics.Controls.Schedules;

namespace Infragistics.AutomationPeers
{
	/// <summary>
	/// Exposes <see cref="CalendarHeaderArea"/> types to UI Automation
	/// </summary>
	public class CalendarHeaderAreaAutomationPeer : FrameworkElementAutomationPeer
		, ISelectionProvider
	{
		#region Constructor
		/// <summary>
		/// Initializes a new <see cref="CalendarHeaderAreaAutomationPeer"/>
		/// </summary>
		/// <param name="owner">The <see cref="CalendarHeaderArea"/> that the peer represents</param>
		public CalendarHeaderAreaAutomationPeer(CalendarHeaderArea owner)
			: base(owner)
		{
		}
		#endregion // Constructor

		#region Base class overrides

		#region GetAutomationControlTypeCore
		/// <summary>
		/// Returns an enumeration indicating the type of control represented by the automation peer.
		/// </summary>
		/// <returns>The <b>Tab</b> enumeration value</returns>
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Tab;
		}

		#endregion //GetAutomationControlTypeCore

		#region GetClassNameCore
		/// <summary>
		/// Returns the name of the <see cref="CalendarHeaderArea"/>
		/// </summary>
		/// <returns>A string that contains 'CalendarHeaderArea'</returns>
		protected override string GetClassNameCore()
		{
			return "CalendarHeaderArea";
		}

		#endregion //GetClassNameCore

		#region GetPattern
		/// <summary>
		/// Returns the control pattern for the element that is associated with this peer.
		/// </summary>
		/// <param name="patternInterface">The pattern being requested</param>
		/// <returns>The pattern provider or null</returns>
		public override object GetPattern(PatternInterface patternInterface)
		{
			if (patternInterface == PatternInterface.Selection)
				return this;

			return base.GetPattern(patternInterface);
		}
		#endregion // GetPattern

		#endregion //Base class overrides

		#region Properties

		#region Element
		internal CalendarHeaderArea Element
		{
			get { return (CalendarHeaderArea)this.Owner; }
		}
		#endregion // Element

		#endregion // Properties

		#region Methods

		#region GetPeer
		private AutomationPeer GetPeer(ResourceCalendar calendar)
		{
			if (null != calendar)
			{
				var items = this.Element.Items as IList<CalendarHeaderAdapter>;

				Debug.Assert(null != items, "Unexpected items type");

				ISupportRecycling item = CalendarHeaderAreaAdapter.GetAdapter(items, calendar);

				if (null != item && null != item.AttachedElement)
					return FrameworkElementAutomationPeer.CreatePeerForElement(item.AttachedElement);
			}

			return null;
		}
		#endregion // GetPeer

		#region RaiseSelectionEvents
		internal void RaiseSelectionEvents(ResourceCalendar oldValue, ResourceCalendar newValue)
		{
			if (newValue != null)
			{
				AutomationPeer peer = this.GetPeer(newValue);

				if (null != peer)
				{
					peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementSelected);
					return;
				}
			}

			if (oldValue != null)
			{
				AutomationPeer peer = this.GetPeer(oldValue);

				if (null != peer)
					peer.RaiseAutomationEvent(AutomationEvents.SelectionItemPatternOnElementRemovedFromSelection);
			}
		}
		#endregion // RaiseSelectionEvents

		#endregion // Methods

		#region ISelectionProvider Members

		bool ISelectionProvider.CanSelectMultiple
		{
			get { return false; }
		}

		IRawElementProviderSimple[] ISelectionProvider.GetSelection()
		{
			var group = this.Element.CalendarGroup;
			var selectedCalendar = null != group ? group.SelectedCalendar : null;
			var peer = GetPeer(selectedCalendar);

			if (peer == null)
				return null;

			return new IRawElementProviderSimple[] { this.ProviderFromPeer(peer) };
		}

		bool ISelectionProvider.IsSelectionRequired
		{
			get { return true; }
		}

		#endregion //ISelectionProvider Members
	}
}

#region Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved
/* ---------------------------------------------------------------------*
*                           Infragistics, Inc.                          *
*              Copyright (c) 2001-2012 All Rights reserved               *
*                                                                       *
*                                                                       *
* This file and its contents are protected by United States and         *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF INFRAGISTICS, INC. *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY INFRAGISTICS PRODUCT.    *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF INFRAGISTICS,      *
* INC.  THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO       *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved