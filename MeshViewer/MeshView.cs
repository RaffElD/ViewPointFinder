using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ViewPointFinderLib;

namespace MeshViewer
{
	public partial class MeshView : Form
	{
		private Mesh  _mesh;
		private float _minValue;
		private float _valueRange;

		private Bitmap _meshDrawBitmap;

		public MeshView()
		{
			InitializeComponent();

			JSONHandler.DeserializeMeshFromJSONFile("mesh.json", ref ViewPointFinder.mesh);
			ViewPointFinder.Initialize();
			this._mesh       =  ViewPointFinder.mesh;
			this._minValue   =  this._mesh.values.Min(v => v.value);
			this._valueRange =  this._mesh.values.Max(v => v.value) - this._minValue;
			this._minValue   *= -1;

			DrawMeshToBitmap();
		}

		private void DrawMeshToBitmap()
		{
			int   scaleFactorX  = 170;
			int   scaleFactorY  = 100;
			Point highestExtent = Point.Empty;
			highestExtent.X      = (int)(this._mesh.nodes.Select(node => node.x).Max() * scaleFactorX);
			highestExtent.Y      = (int)(this._mesh.nodes.Select(node => node.y).Max() * scaleFactorY);
			this._meshDrawBitmap = new Bitmap(highestExtent.X, highestExtent.Y);
			Value[]  viewPoints = ViewPointFinder.GetViewPointsSorted();
			Gradient gradient   = new Gradient(Color.Beige, Color.Firebrick);
			using (Graphics g = Graphics.FromImage(this._meshDrawBitmap))
			{
				var pen   = new Pen(Color.Aqua);
				var brush = new SolidBrush(Color.Empty);
				foreach (Element meshElement in _mesh.elements)
				{
					var points = meshElement.nodes.Select(node =>
					{
						var newPoint = new Point((int)(this._mesh.nodes[node].x * scaleFactorX),
														 (int)(this._mesh.nodes[node].y * scaleFactorY));
						return newPoint;
					}).ToArray();

					float percentage = (meshElement.valueRef.value + this._minValue) / this._valueRange;
					brush.Color = gradient.GetColorAtPercentage(percentage);
					g.FillPolygon(brush, points);
				}

				foreach (Value viewPoint in viewPoints)
				{
					Node coordsSum = this._mesh.elements.Single(elem => elem.id == viewPoint.element_id)
						.nodes.Select(nodeId => this._mesh.nodes.Single(node => node.id == nodeId))
						.Aggregate((p1, p2) => new Node() { x = p1.x + p2.x, y = p1.y + p2.y });
					Point elementCenter = new Point((int)(coordsSum.x / 3 * scaleFactorX), 
															  (int)(coordsSum.y / 3 * scaleFactorY));
					var rect = new Rectangle(elementCenter, new Size(16, 16));
					rect.Offset(-8, -8);
					brush.Color = Color.Aqua;
					g.FillEllipse(brush, rect);
				}
			}

			this.panelDrawArea.Size = Size.Ceiling(new SizeF(highestExtent));
		}

		private void panelDrawArea_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(this._meshDrawBitmap, Point.Empty);
			e.Graphics.Dispose();
		}

		private void MeshView_FormClosing(object sender, FormClosingEventArgs e)
		{
			this._meshDrawBitmap.Dispose();
		}
	}
}