import { createApp } from 'vue'
import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router'

import App from './App.vue'
import Wln from '@/wlnapp/wln.js'
import ElementPlus from 'element-plus'
import zhCn from 'element-plus/dist/locale/zh-cn.mjs'
import 'element-plus/dist/index.css'
import { ElMessage, ElMessageBox} from 'element-plus'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
const app = createApp(App)
const router = createRouter({ history: createWebHistory('/'), routes: [] })
const cfgs = { api: 'http://127.0.0.1:5000/control', debug: localStorage.getItem('debug') == 'true', pk: localStorage.getItem('pub') || '' }
const wln = Wln(cfgs, {
    noauth: (r) => {
        wln.alert('未登录，请先登录',() => {console.log('by noauth')})
    },
    toast: (msg, type) => {
        ElMessage({ message: msg, grouping: true, duration: 3000, type: type || 'info' })
    },
    alert: (msg, fnOk) => {
        ElMessageBox.alert(msg, '提示', { callback: fnOk, confirmButtonText: '确定' })
    },
    confirm: (msg, fnYes, fnNot, txtYes, txtNot) => {
        ElMessageBox.confirm(msg, '操作确认', { confirmButtonText: txtYes, cancelButtonText: txtNot }).then(fnYes).catch(fnNot)
    },
	goback: (delta) => {
		router.back(delta)
	},
    gourl: (url, type) => {
        if (url.indexOf('://') < 0) {
            router.push(url)
        } else {
            return location.href = url
        }
	}
})
app.use(router)
app.use(ElementPlus, { locale: zhCn })
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}
app.config.globalProperties.$wln = wln
wln.api('/authx/token' + location.search, (res) => {
    if(res.success) {
        localStorage.setItem('ticket', res.ticket)
        localStorage.setItem('x-domain', res.domain)
    }
    app.mount('#app')
}, {}, false, true)
router.isReady().then(() => {
    router.addRoute({ path: '/', component: () => import('@/views/home.vue') })
    router.addRoute({
        path: '/content',
        component: () => import('@/views/content/_layout.vue'),
        children: [
            { path: 'category_list', component: () => import('@/views/content/category_list.vue') },
        ]
    })
    router.replace(router.currentRoute.value.fullPath)
})