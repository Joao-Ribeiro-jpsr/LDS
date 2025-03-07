const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:7115",
    secure: false,
    pathRewrite: {
      "^/": ""
    }
  }
]

module.exports = PROXY_CONFIG;
