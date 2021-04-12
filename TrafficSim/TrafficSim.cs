using System;
using System.Collections.Generic;
using TrafficSim.Conf;
using System.Linq;
using System.Net.Http;

namespace TrafficSim
{
    internal class TrafficSim
    {
        private readonly Random _randomizer;
        private TrafficSimState _current;
        private readonly Dictionary<string, TrafficSimState> _states;
        private Action<string> _loggerCallback;
        private HttpClient _httpClient;

        public TrafficSim(TrafficSimConf simConf, Action<string> loggerCallback, HttpClient httpClient)
        {
            _randomizer = new Random(DateTime.Now.Millisecond);
            _current = simConf.states[0];
            _states = new Dictionary<string, TrafficSimState>();
            simConf.states.ForEach(state => _states.Add(state.name, state));
            _loggerCallback = loggerCallback;
            _httpClient = httpClient;

            makeTheCall(_current.path);
        }

        public void Update()
        {
            _loggerCallback("");
            _loggerCallback("Thinking about what to do next...");

            var weightSum = _current.steps.Sum(step => step.weight);
            var nextActionIndexer = _randomizer.Next(weightSum+1);
            foreach (var step in _current.steps)
            {
                weightSum -= step.weight;
                if (weightSum <= nextActionIndexer)
                {
                    _loggerCallback($"Decided to '{step.action}'");

                    var nextState = _states[step.to];
                    makeTheCall(nextState.path);
                    _current = nextState;

                    break;
                }
            }
        }

        private void makeTheCall(string path)
        {
            _loggerCallback($"Requesting '{path}'");
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            _httpClient.SendAsync(request).Wait();
        }
    }
}
