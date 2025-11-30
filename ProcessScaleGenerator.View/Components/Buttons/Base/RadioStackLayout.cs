using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace ProcessScaleGenerator.View.Components.Buttons.Base
{
    internal class RadioStackLayout : StackLayout
    {
        private readonly List<IconButton> _todosBotoes = new List<IconButton>();

        public static readonly BindableProperty SelectionToleranceProperty =
            BindableProperty.Create(nameof(SelectionTolerance), typeof(int), typeof(RadioStackLayout), defaultValue: 1);

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(RadioStackLayout), defaultValue: Colors.Orange);

        public static readonly BindableProperty UnselectedColorProperty =
            BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(RadioStackLayout), defaultValue: Colors.Transparent);

        public int SelectionTolerance
        {
            get => (int)GetValue(SelectionToleranceProperty);
            set => SetValue(SelectionToleranceProperty, value);
        }
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }
        public Color UnselectedColor
        {
            get => (Color)GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            // Em vez de checar só o filho direto, iniciamos o scanner recursivo
            EscanearERegistrar(child);
        }

        // --- LÓGICA RECURSIVA ---
        private void EscanearERegistrar(Element elemento)
        {
            // 1. É UM BOTÃO?
            if (elemento is IconButton btn)
            {
                // Evita duplicidade se o layout for redesenhado
                if (!_todosBotoes.Contains(btn))
                {
                    _todosBotoes.Add(btn);
                    ConfigurarEventoDeClique(btn);
                    AplicarVisual(btn, false); // Estado inicial
                }
                return;
            }

            // 2. É UM CONTAINER (Grid, StackLayout, etc)?
            if (elemento is Layout layout)
            {
                // Varre quem já está lá dentro
                foreach (var filho in layout.Children)
                {
                    if (filho is Element el) EscanearERegistrar(el);
                }

                // Monitora futuros filhos adicionados dinamicamente neste container
                layout.ChildAdded += (s, e) => EscanearERegistrar(e.Element);
            }

            // 3. É UM CONTENTVIEW (Border, Frame, etc)?
            else if (elemento is ContentView cv && cv.Content != null)
            {
                EscanearERegistrar(cv.Content);
            }
        }

        // --- LÓGICA DE GESTURE (A sua correção original) ---
        private void ConfigurarEventoDeClique(IconButton btn)
        {
            if (btn.Content is Border conteudoInterno) // Geralmente é o Border
            {
                var gestoExistente = conteudoInterno.GestureRecognizers
                                        .OfType<TapGestureRecognizer>()
                                        .FirstOrDefault();

                if (gestoExistente != null)
                {
                    // Usa o gesto existente (preserva o Command do usuário)
                    gestoExistente.Tapped += (s, e) => ProcessarSelecao(btn);
                }
                else
                {
                    // Cria um novo se não existir
                    var novoGesto = new TapGestureRecognizer();
                    novoGesto.Tapped += (s, e) => ProcessarSelecao(btn);
                    conteudoInterno.GestureRecognizers.Add(novoGesto);
                }
            }
        }

        private void ProcessarSelecao(IconButton btnClicado)
        {
            // AQUI MUDOU: Varremos a nossa lista plana (_todosBotoes)
            // em vez de varrer 'Children' (que só pegaria o Grid pai)
            foreach (var btn in _todosBotoes)
            {
                bool ehOSelecionado = (btn == btnClicado);
                AplicarVisual(btn, ehOSelecionado);
            }
        }

        private void AplicarVisual(IconButton btn, bool selecionado)
        {
            if (selecionado)
            {
                btn.BackgroundColor = SelectedColor;
                btn.UnicodeFamily = "PPS";
                btn.ScaleTo(1, 100);
                btn.Padding = 5;
            }
            else
            {
                btn.BackgroundColor = UnselectedColor;
                btn.UnicodeFamily = "PPO"; 
                btn.ScaleTo(0.95, 100);
                btn.Padding = 2;
            }
        }
    }
}