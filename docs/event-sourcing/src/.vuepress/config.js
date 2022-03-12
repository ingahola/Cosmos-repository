const { description } = require('../../package')

module.exports = {
  base: '/cosmos-event-sourcing-docs/',
  title: 'Cosmos Event Sourcing',
  description: description,
  head: [
    ['meta', { name: 'theme-color', content: '#3eaf7c' }],
    ['meta', { name: 'apple-mobile-web-app-capable', content: 'yes' }],
    ['meta', { name: 'apple-mobile-web-app-status-bar-style', content: 'black' }]
  ],
  themeConfig: {
    logo: '/small-logo.jpg',
    repo: 'https://github.com/IEvangelist/azure-cosmos-dotnet-repository',
    editLinks: false,
    docsDir: '',
    editLinkText: '',
    lastUpdated: false,
    nav: [
      {
        text: 'Getting Started',
        link: '/getting-started/',
      },
      {
        text: 'Samples',
        link: '/samples/'
      },
      {
        text: 'Discord',
        link: 'https://discord.com/invite/qMXrX4shAv'
      }
    ],
    sidebar: [
      {
        title: 'Getting Started',
        path: '/getting-started/',
        collapsable: true
      },
      {
        title: 'Features',
        path: '/features/',
        collapsable: false,
        children: [
          
        ]
      },
      {
        title: 'Tutorial',
        path: '/tutorial/',
        collapsable: false,
        children: [
          
        ]
      },
    ]
  },

  /**
   * Apply plugins，ref：https://v1.vuepress.vuejs.org/zh/plugin/
   */
  plugins: [
    '@vuepress/plugin-back-to-top',
    '@vuepress/plugin-medium-zoom',
  ]
}