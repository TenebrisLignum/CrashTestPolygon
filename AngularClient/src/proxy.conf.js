const PROXY_CONFIG = [
  {
    context: ['/api'],
    target: "https://localhost:44373",
    secure: true
  }
]

module.exports = PROXY_CONFIG;
