import Vue from 'vue'
import F2 from './Feature2.vue'

Vue.config.productionTip = false

new Vue({
    render: h => h(F2), 
}).$mount('#f2App')
