export class SKCanvas2DContext {

  init(ctx) {
    this.ctx = ctx;
    this.ctx.lineCap = 'round';
  }

  stroke = (stroke) => {
    console.log(stroke);
    this.setStrokeStyle(stroke.style);
    this.line(stroke.stylusPoints);
  }

  wipe = (wipe) => {
    this.setWipeStyle(wipe.style);
    this.line(wipe.stylusPoints);
  }

  fill = (fill) => {
    this.setFillStyle(fill.style);
    this.ctx.fillRect(0, 0, Number.MAX_VALUE, Number.MAX_VALUE);
    this.ctx.fill('evenodd');
  }

  clear = (clear) => {
    this.ctx.clearRect(clear.x, clear.y, clear.width, clear.height);
  }

  line(points) {

    points.forEach((point, index) => {
      if (index === 0) {
          this.ctx.beginPath();
          this.ctx.moveTo(point.x, point.y);
      } else {
        console.log(point);
        this.ctx.lineTo(point.x, point.y);
      }
    });

    this.ctx.stroke();
  }

  setStrokeStyle(strokeStyle) {
    const {r, g, b} = strokeStyle.color;

    this.ctx.lineWidth = strokeStyle.thickness;
    this.ctx.globalCompositeOperation = 'source-over';
    this.ctx.strokeStyle = "#" + ((1 << 24) + (r << 16) + (g << 8) + b).toString(16).slice(1);
  }

  setWipeStyle(wipeStyle) {
    this.ctx.lineWidth = wipeStyle.thickness;
    this.ctx.globalCompositeOperation = 'destination-out';
  }

  setFillStyle(fillStyle) {
    const {r, g, b} = fillStyle.color;
    this.ctx.globalCompositeOperation = 'destination-out';
    this.ctx.strokeStyle = "#" + ((1 << 24) + (r << 16) + (g << 8) + b).toString(16).slice(1);
  }
}