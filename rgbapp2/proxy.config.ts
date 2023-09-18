module.exports = [
  {
    context: ['/rgbSocket'],
    logLevel: 'debug',
    ws: true,
    target: 'http://localhost:5062',
  },
  {
    context: ['/rgb'],
    logLevel: 'debug',
    target: 'http://localhost:5062',
  },
]