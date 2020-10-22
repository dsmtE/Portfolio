class CanvasWrapper {
  constructor(image) {
    this.canvas = document.createElement('canvas');
    this.context = this.canvas.getContext('2d');
    this.canvas.width = image.naturalWidth;
    this.width = this.canvas.width;
    this.canvas.height = image.naturalHeight;
    this.height = this.canvas.height;
    this.context.drawImage(image, 0, 0, this.width, this.height);
  }

  getImageData() {
    return this.context.getImageData(0, 0, this.width, this.height);
  }
}

const getAverage = (image, stride) => {
  const canvas = new CanvasWrapper(image);

  const imageData = canvas.getImageData();
  // improve perf by using less generic way than:
  // const pixels = groupConsecutive(imageData.data, 4);
  // const sampledPixels = subStride(pixels, stride);
  const sampledPixels = [];
  for (let i = 0; i * 4 + 4 <= imageData.data.length; i += stride) {
    sampledPixels.push(imageData.data.slice(i * 4, i * 4 + 4));
  }

  return sampledPixels // averages sum of squared values
    .reduce((acc, value) => acc.map((v, i) => v + value[i] * value[i]), [0, 0, 0, 0])
    .map(x => Math.sqrt(x / sampledPixels.length));
};

export { getAverage };
