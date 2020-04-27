
import Vue from 'vue'
import F4 from './Feature4.vue'

Vue.config.productionTip = false

new Vue({
    render: h => h(F4),
}).$mount('#f4App')