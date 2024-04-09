using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ActiveReports.Samples.ReportWizard.UI 
{
	public class DragDropListBox : ListBox
	{
		public class DataObjectEventArgs : EventArgs
		{
			private object _dataObject;
			private readonly Point _mouseLocation;

			public DataObjectEventArgs(Point pt)
			{
				_mouseLocation = pt;
			}

			public Point MouseLocation
			{
				get { return _mouseLocation; }
			}

			public object DataObject
			{
				get { return _dataObject; }
				set { _dataObject = value; }
			}
		}

		public class DragDropEffectsEventArgs : EventArgs
		{
			private DragDropEffects _effects = DragDropEffects.None;

			public DragDropEffects DragDropEffects
			{
				get { return _effects; }
				set { _effects = value; }
			}
		}

		public class DragDropEventArgs : EventArgs
		{
			private readonly DragDropEffects _allowedEffects;
			private readonly DragDropEffects _effects;
			private readonly Point _mouseLocation;
			private readonly object _data;

			public DragDropEventArgs( object data, Point mouseLocation, DragDropEffects allowedEffects, DragDropEffects effects )
			{
				_data = data;
				_mouseLocation = mouseLocation;
				_allowedEffects = allowedEffects;
				_effects = effects;
			}

			public object Data
			{
				get { return _data; }
			}

			public Point MouseLocation
			{
				get { return _mouseLocation; }
			}

			public DragDropEffects Effects
			{
				get { return _effects; }
			}
			public DragDropEffects AllowedEffects
			{
				get { return _allowedEffects; }
			}
		}

		public class AllowDropEventArgs : EventArgs
		{
			public bool AllowDrop;
		}

		private static readonly Point NotDragging = new Point(-1, -1);

		private Point _startDragLocation;

		[Browsable(true)]
		[Category("Drag Drop")]
		[Description("コントロールからドラッグするデータを取得する時に発生します。")]
		public event EventHandler<DataObjectEventArgs> GetDataObject;
		
		protected virtual void OnGetDataObject(DataObjectEventArgs e)
		{
			EventHandler<DataObjectEventArgs> handler = GetDataObject;
			
			if (handler != null)
				handler(this, e);
		}

		[Browsable(true)]
		[Category("Drag Drop")]
		[Description("ドラッグの効果を取得する時に発生します。")]
		public event EventHandler<DragDropEffectsEventArgs> GetDragEffects;

		protected virtual void OnGetDragEffects(DragDropEffectsEventArgs e)
		{
			EventHandler<DragDropEffectsEventArgs> handler = GetDragEffects;
			if (handler != null)
				handler(this, e);
		}
		
		[Browsable(true)]
		[Category("Drag Drop")]
		[Description("ドラッグ・ドロップの操作が完了される時に発生します。")]
		public event EventHandler<DragDropEventArgs> DragCompleted;

		protected virtual void OnDragCompleted(DragDropEventArgs e)
		{
			EventHandler<DragDropEventArgs> handler = DragCompleted;
			if (handler != null)
				handler(this, e);
		}

		[Browsable( true )]
		[Category( "Drag Drop" )]
		[Description( "コントロールからデータがドラッグされた後、ドロップ操作が許可されているかどうかを決定する時に発生します。" )]
		public event EventHandler<AllowDropEventArgs> AllowDropOperation;

		protected virtual void OnAllowDropOperation(AllowDropEventArgs e)
		{
			EventHandler<AllowDropEventArgs> handler = AllowDropOperation;
			if (handler != null)
				handler(this, e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (IsLeftMouseButton(e.Button))
			{
				_startDragLocation = e.Location;
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (IsLeftMouseButton(e.Button))
			{
				_startDragLocation = NotDragging;
			}
			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (IsLeftMouseButton(e.Button) && _startDragLocation != NotDragging)
			{
				// ドラッグ・ドロップの操作を開始する前に設定を確認します。
				Size sz = new Size(Math.Abs(_startDragLocation.X - e.Location.X), Math.Abs(_startDragLocation.Y - e.Location.Y));
				if (sz.Height > SystemInformation.DragSize.Height || sz.Width > SystemInformation.DragSize.Width)
				{
					BeginDragDrop(_startDragLocation);
				}
			}
		}

		private void BeginDragDrop(Point startingPoint)
		{
			DataObjectEventArgs e = new DataObjectEventArgs(startingPoint);
			OnGetDataObject(e);
			object data = e.DataObject;
			if (data == null)
				return;
			DragDropEffectsEventArgs de = new DragDropEffectsEventArgs();
			OnGetDragEffects(de);
			DragDropEffects allowedEffects = de.DragDropEffects;
			DragDropEffects effects = DoDragDrop( data, allowedEffects );
			DragDropEffectsEventArgs finishedArgs = new DragDropEffectsEventArgs();
			finishedArgs.DragDropEffects = effects;
			DragDropEventArgs ddea = new DragDropEventArgs( data, startingPoint, allowedEffects, effects );
			OnDragCompleted( ddea );
		}

		private static bool IsLeftMouseButton( MouseButtons buttons )
		{
			return 0 != (buttons & MouseButtons.Left);
		}

		private void InitializeComponent()
		{
			SuspendLayout();
			ResumeLayout(false);
		}
	}
}
