<div style="text-align: center;">
<h1> AR2-D2: Training a Robot without a Robot</h1>

 [Jiafei Duan](https://duanjiafei.com/)$^1$, [Yi Ru Wang](https://helen9975.github.io/)$^1$, [Mohit Shridhar](https://mohitshridhar.com/)$^1$, [Abhimanyu Saighal](https://www.linkedin.com/in/abhimanyu-saighal-926941292)$^1$, [Dieter Fox](https://homes.cs.washington.edu/~fox/)$^{1,2}$, , [Ranjay Krishna](https://ranjaykrishna.com/index.html/)$^{1,3}$

$^1$ University of Washington, $^2$ NVIDIA, $^3$ Allen Institute for AI 

[Project Page](https://ar2d2.site/) | [Arxiv](https://arxiv.org/abs/2306.13818) 

<div style="margin:50px; text-align: justify;">
<img style="width:100%;" src="imgs/r12-ezgif.com-video-to-gif-converter.gif">

If you find this codebase useful, consider citing:

```bibtex
@article{duan2023ar2,
  title={Ar2-d2: Training a robot without a robot},
  author={Duan, Jiafei and Wang, Yi Ru and Shridhar, Mohit and Fox, Dieter and Krishna, Ranjay},
  journal={arXiv preprint arXiv:2306.13818},
  year={2023}
}
```

🌟 **AR2-D2: Training a Robot without a Robot** 🌟

AR2-D2 is a robot demonstrations collection framework in the form of an iOS app that people can use to project an AR robot into the physical world and record a video of themselves manipulating any object whilst simultaneously capturing the essential data modalities for training a real robot


❓ If you have any questions, please contact [me](https://duanjiafei.com/) at `duanj1 [at] cs [dot] washington [dot] edu`. ❓

AR2-D2 APP Download [here](https://drive.google.com/drive/folders/17fN4YfXNuD-L05oBoDvrJ8MzjUqIOqfn?usp=share_link)

Installation instruction [here](https://docs.google.com/document/d/17PS8zkP4EkMilSVw9wQCFGUU0mUgzD5S5Ya1lV3-hMw/edit?usp=sharing)


## :hammer: AR2-D2 IOS APP Setup
<details open>
<summary>[Click to view]</summary>

```
1. Download all the folders in the  AR2-D2 APP
2. Follow the step-by-step instructions for installation.
```
</details>

## AR2-D2 utils
1. We have provided two interactive notebooks in the utilities 
folder - one to generate training data from the app's raw output and one to visualise a particular scene in voxel form
2. To use the data generation notebook, first organise the app's raw output (i.e., the depth images, rgb images, and text) into folders by episode. There should be one top-level directory and several sub-folders, each corresponding to a given episode, like so:
    ```
    SceneName
    |
    |__ episode1
    |    |__ (rgb + depth + text files)
    |
    |__ episode2
        ...
    ```
    Note that the sub-folders must follow the naming convention episode{EPISODE_NUMBER}, where {EPISODE_ NUMBER} starts from 1
3. Once you have generated the training data using the first notebook, you can visualise each episode in voxel form using the second notebook. Simply specify the name of the directory containing the training data and the episode number.

## Acknowledgements

We thank the members of the Robotics State Estimation lab and Krishna’s group for the helpful discussions and feedback on the paper. Jiafei Duan is supported by the National Science Scholarship from The Agency for Science, Technology and Research (A*STAR), Singapore.

## Coming soon...

<details open>
<summary>[Click to view]</summary>

* Docker training pipeline
* PerAct real-world implementation

</details>
