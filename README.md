# SolarSystemOrbits
A repository for the BPhO computational challenge entry 2023.
ToDos and project management is in the projects tab.

## Plan
### UI framework in WPF
- Use MVVM design pattern (UI as view, backend C++ as model, intermediate C# code as ViewModel)
- Home screen to select tasks
- Adaptive changing variables (update on lost focus TextBoxes)
- Use LiveCharts until it is insufficient (hehe) - hopefully it can animate as well
### Backend computation in C++ (for earlier challenges) or CUDA, built along with c# for intercommunication with UI
- Expose functions to C#, UI will pass parameters (from WPF View to C++ Model)
- GPU and non-GPU versions in case no GPU to run on (these will be differentiated in backend, same function exposed to ViewModel)
- Backend computation should be fast enough to allow for UI updates relatively smoothly.
### Overleaf for paper - this will describe our computational solution
