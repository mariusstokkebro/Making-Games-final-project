using System;
using UnityEngine;

public class DoppelGanger : BaseEnemy
{
    private Transform player;
    private Vector3 lastPlayerPos;

    [SerializeField] private GameObject Projectile;
    [SerializeField] private float range = 50f;
    [SerializeField] private float timeBetweenShots = 5f;
    private float lastShot = 0f;

    protected override void OnActivated()
    {
        player = FindPlayer();
        if (player != null)
        {
            lastPlayerPos = player.position;
        }
        lastShot = Time.time;
    }

    void Update()
    {
        if (!isActive || player == null) return;

        ReflectPlayerMovement();
        TurnTowardsTarget(player.position);
        Shoot();
    }

    private void ReflectPlayerMovement()
    {
        Vector3 playerDelta = player.position - lastPlayerPos;

        Vector3 reflectedDelta = -playerDelta;

        Vector3 targetPos = transform.position + reflectedDelta;

        MoveTowardsTarget(targetPos);

        lastPlayerPos = player.position;
    }

    private void Shoot()
    {
        if (Time.time - lastShot < timeBetweenShots) return;
        if (Vector3.Distance(transform.position, player.position) > range) return;

        lastShot = Time.time;

        Vector3 spawnPos = transform.position + transform.forward * 1f;

        GameObject proj = Instantiate(Projectile, spawnPos, Quaternion.identity);

        if (proj.TryGetComponent<SpearProjectile>(out var spearProj))
        {
            spearProj.Initialize(damage, 1f, 1f, 3f);
            spearProj.SetTarget(player);
        }
    }
}
