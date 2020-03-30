const config = {
    baseURL: 'https://localhost:44320/api/',
    baseURLWeb: 'https://localhost:44316'
};

Vue.use(window.vuelidate.default);
Vue.use(VueMask.VueMaskPlugin);

const UNAUTHORIZED = 401;
let timerInterval;

axios.interceptors.response.use(
    response => response,
    error => {
        const { status } = error.response;
        if (status === UNAUTHORIZED) {
            localStorage.clear();

            //Swal.fire({
            //    type: 'warning',
            //    title: 'Tempo de Acesso Expirado',
            //    html: '<p>Suas credencias expiraram.</p><p>Será necessário efetuar um novo Login.</p>',
            //    showConfirmButton: false,
            //    timer: 3000,
            //    onClose: () => {
            //        clearInterval(timerInterval);
            //        window.location.href = config.baseURLWeb + '/home/login';
            //    }
            //});

            console.log(error.response);

            Swal.fire({
                type: 'warning',
                title: 'Acesso Negado',
                html: '<p>O seu usuário não tem acesso à esse recurso.</p><p>' + error.response + '</p>',
                showConfirmButton: false,
                timer: 3000,
                onClose: () => {
                    clearInterval(timerInterval);
                    //window.location.href = config.baseURLWeb + '/home/login';
                }
            });

        }
        return Promise.reject(error);
    }
);

const api = {
    get(url, auth = true) {
        url = config.baseURL + url;
        const method = 'get';
        const headers = {};
        if (auth) {
            headers['Authorization'] = 'Bearer ' + this.token;
        }
        return axios({ method, url, headers }).then(res => res);
    },

    post(url, data, auth = true) {
        url = config.baseURL + url;
        const method = 'post';
        const headers = {};
        if (auth) {
            headers['Authorization'] = 'Bearer ' + this.token;
        }
        return axios({ method, url, data }).then(res => res);
    },

    put(url, data, auth = true) {
        url = config.baseURL + url;
        const method = 'put';
        const headers = {};
        if (auth) {
            headers['Authorization'] = 'Bearer ' + this.token;
        }
        return axios({ method, url, data }).then(res => res);
    },

    delete(url, data, auth = true) {
        url = config.baseURL + url;
        const method = 'delete';
        const headers = {};
        if (auth) {
            headers['Authorization'] = 'Bearer ' + this.token.jwt;
        }
        return axios({ method, url, data }).then(res => res);
    },

    set token(token) {
        window.localStorage.setItem('token', JSON.stringify(token));
        //const validade = (new Date(token.validade).getTime() - new Date().getTime()) / 1000;
        //document.cookie = 'token=' + token.jwt + '; max-age= ' + validade + '; path=/';
    },

    get token() {
        return JSON.parse(window.localStorage.getItem('token'));
    },

    aguarde() {

        $('#aguarde').modal({ backdrop: 'static', keyboard: false })  
    },

    fecharAguarde() {

        $('#aguarde').modal('hide');
    },

    mensagem(texto) {

        $('#conteudoMensagem').html(texto);
        $('#mensagem').modal({ backdrop: 'static', keyboard: false });
    },
};

Vue.mixin({
    methods: {

        invalido(...props) {
            let campo = this.$v;
            let nomeCampo = '';

            for (let prop of props) {
                campo = campo[prop];
                nomeCampo = prop;
            }
            return campo.$dirty && campo.$invalid || this.errors[nomeCampo];
        },

        erro(...props) {
            let campo = this.$v;

            for (let prop of props) {
                campo = campo[prop];
            }

            return !campo;
        },

        getId() {
            const id = window.location.pathname.split('/')[window.location.pathname.split('/').length - 1];

            if (isNaN(parseInt(id)))
                return null;
            else
                return id;
        },

        validaCNPJ(cnpj) {

            cnpj = cnpj.replace(/[^\d]+/g, '');

            if (cnpj === '') return false;

            if (cnpj.length != 14)
                return false;

            // Elimina CNPJs invalidos conhecidos
            if (cnpj === "00000000000000" ||
                cnpj === "11111111111111" ||
                cnpj === "22222222222222" ||
                cnpj === "33333333333333" ||
                cnpj === "44444444444444" ||
                cnpj === "55555555555555" ||
                cnpj === "66666666666666" ||
                cnpj === "77777777777777" ||
                cnpj === "88888888888888" ||
                cnpj === "99999999999999")
                return false;

            // Valida DVs
            tamanho = cnpj.length - 2;
            numeros = cnpj.substring(0, tamanho);
            digitos = cnpj.substring(tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(0))
                return false;

            tamanho = tamanho + 1;
            numeros = cnpj.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos.charAt(1))
                return false;

            return true;
        },

        getPermissao(funcionalidade) {

            api.get('permissao/getByFuncionalidade/' + funcionalidade, true)
                .then(({ data, errors }) => {
                    this.errors = errors || {};
                    if (errors) {
                        return;
                    }

                    return data;
                });
        }
    }
});