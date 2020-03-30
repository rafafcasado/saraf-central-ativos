const { required, email, minLength } = window.validators;

new Vue({
    el: '#login-form',
    data() {
        return {
            email: '',
            senha: '',
            errors: {},
        };
    },

    validations: {
        email: {
            required,
            email,
        },
        senha: {
            required,
            minLength: minLength(4),
        },
    },

    methods: {

        login() {
            // verifica as validações
            this.$v.$touch();

            if (this.$v.$invalid) {
                return;
            }

            Swal.fire({
                title: 'Aguarde, por favor.',
                html: '<p>Estamos processando sua requisição</p>',
                onBeforeOpen: () => {

                    Swal.showLoading();
                }
            });

            const { email, senha } = this;  //campos

            var reqData = "grant_type=password&username=" + email + "&password=" + senha;

            axios.request({
                url: config.baseURL + "token",
                method: 'post',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                data: (reqData)
            }).then(function (res) {

                window.localStorage.setItem('token', JSON.stringify(res.data.access_token));

                api.get('helpers/getUsuarioLogado', true)
                    .then(({ data, errors }) => {
                        this.errors = errors || {};
                        if (errors) {
                            return;
                        }

                        window.localStorage.userName = data.Nome;

                        if (data.EmpresaID) {
                            window.localStorage.setItem('empresaID', data.EmpresaID);
                            window.localStorage.setItem('empresaNome', data.Empresa.NomeFantasia);
                        }

                        if (data.PerfilID === 1) //Administrador
                            window.location.href = config.baseURLWeb + '/';
                        else
                            window.location.href = config.baseURLWeb + '/Home/Dashboard';

                    });

            }).catch(function (erro) {

                Swal.fire({
                    type: 'error',
                    title: 'Erro',
                    text: erro.response.data.error_description
                });
            });

        }
    },
    mounted() {

        localStorage.clear();
    }

});